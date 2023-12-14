using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Services {

    public class RealEstateService : IRealEstateService {

        private const string CACHE_KEY_TOP_TEN_REAL_ESTATE = "top_ten_real_estate_agents";
        private const string CACHE_KEY_TOP_TEN_REAL_ESTATE_WITH_GARDEN = "top_ten_real_estate_agents_with_garden";
        private ApiService apiService;
        public CacheService cacheService;

        public RealEstateService(ApiService apiService, CacheService cacheService) {
            this.apiService = apiService;
            this.cacheService = cacheService;
        }

        public async Task<List<KeyValuePair<int, RealEstate>>> GetTopRealEstates(bool withGarden = false) {
            // Try to get the top ten from cache from a previous elaboration for performance and optimization 
            List<KeyValuePair<int, RealEstate>> topTenRealEstates = GetTopRealEstatesFromCache(withGarden);
            // If cache is empty, proceed with the elaboration
            if ( topTenRealEstates == null ) {
                // Get all the houses from API
                List<BovensteVerdieping.Object> houses = await apiService.GetHouses(withGarden);
                // Get the RealEstate (makelaar) informations and count the houses for each one
                Dictionary<int, RealEstate> realEstates = new Dictionary<int, RealEstate>();
                houses.ForEach(delegate(BovensteVerdieping.Object house)
                    {
                        int realEstateId = house.MakelaarId;
                        if ( realEstates.ContainsKey(realEstateId) ) {
                            RealEstate realEstate = realEstates[realEstateId];
                            realEstate.houses += 1;
                            realEstates[realEstateId] = realEstate;
                        } else {
                            RealEstate realEstate = new RealEstate(realEstateId, house.MakelaarNaam, 1);
                            realEstates.Add(realEstateId, realEstate);
                        }
                    });
                //  Get only the first 10 elements sorted by house number
                List<KeyValuePair<int, RealEstate>> realEstatesList = realEstates.ToList();
                topTenRealEstates = realEstatesList.OrderByDescending(i => i.Value.houses).Take(10).ToList<KeyValuePair<int, RealEstate>>();
                // Set the elaborated result in cache for the future utilisation
                SetTopRealEstatesFromCache(topTenRealEstates, withGarden);
            }
            return topTenRealEstates;
        }
        public async Task<List<KeyValuePair<int, RealEstate>>> GetTopRealEstatesWithGarden() {
            return await GetTopRealEstates(true);
        }

        public List<KeyValuePair<int, RealEstate>> GetTopRealEstatesFromCache(bool withGarden = false) {
            // Get the proper cache key
            string cacheKey = withGarden == false ? CACHE_KEY_TOP_TEN_REAL_ESTATE : CACHE_KEY_TOP_TEN_REAL_ESTATE_WITH_GARDEN;
            // Obtain the object from the cache
            return cacheService.get<List<KeyValuePair<int, RealEstate>>>(cacheKey);
        }

        public void SetTopRealEstatesFromCache(List<KeyValuePair<int, RealEstate>> list, bool withGarden = false) {
            // Get the proper cache key
            string cacheKey = withGarden == false ? CACHE_KEY_TOP_TEN_REAL_ESTATE : CACHE_KEY_TOP_TEN_REAL_ESTATE_WITH_GARDEN;
            // Set the object to the cache with expiration of 5 minutes
            cacheService.set<List<KeyValuePair<int, RealEstate>>>(cacheKey, list, 5);
        }

    }
    
    public class RealEstate {
        public int id { get; set; }
        public string name { get; set; }
        public int houses { get; set; }

        public RealEstate() {}

        public RealEstate(int id, string name, int houses) {
            this.id = id;
            this.name = name;
            this.houses = houses;
        }
    }

}