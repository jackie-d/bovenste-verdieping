using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Services {

    public class RealEstateService {

        private ApiService apiService;

        public RealEstateService(ApiService apiService) {
            this.apiService = apiService;
        }

        public async Task<List<KeyValuePair<int, RealEstate>>> GetTopRealEstates(bool withGarden = false) {
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
            List<KeyValuePair<int, RealEstate>> topTenRealEstates = realEstatesList.OrderByDescending(i => i.Value.houses).Take(10).ToList<KeyValuePair<int, RealEstate>>();
            return topTenRealEstates;
        }

    }
    
    public class RealEstate {
        public int id { get; set; }
        public string name { get; set; }
        public int houses { get; set; }

        public RealEstate(int id, string name, int houses) {
            this.id = id;
            this.name = name;
            this.houses = houses;
        }
    }

}