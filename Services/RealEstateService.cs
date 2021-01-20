using System.Threading.Tasks;

namespace Services {

    public class RealEstateService {

        private ApiService apiService;

        public RealEstateService(ApiService apiService) {
            this.apiService = apiService;
        }

        public async void GetTopRealEstates(bool withGarden = false) {
            await apiService.GetHouses();   
        }

    }

}