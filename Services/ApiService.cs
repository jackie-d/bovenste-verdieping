using System.Net.Http;
using System.Net.Http.Json;
using AppConfiguration = BovensteVerdieping.AppConfiguration;
using System.Collections.Generic;
using System.Web;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Services {

    public class ApiService {

        private readonly string ENPOINT_URL = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/%%APIKEY%%/";
        
        private readonly string SEARCH_VALUE_WITHOUT_GARDEN = "/amsterdam/tuin/";
        private readonly string SEARCH_VALUE_WITH_GARDEN = "/amsterdam/";
        
        private AppConfiguration appConfiguration;
        private HttpClient httpClient;

        public ApiService(AppConfiguration appConfiguration) {
            httpClient = new HttpClient();
            this.appConfiguration = appConfiguration;
        }

        // public accessible methods to retrieve API data

        public async Task<List<BovensteVerdieping.Object>> GetHouses(bool withGarden = false)
        {
            // get the complete URL
            string url = getApiUrlWithParameters(withGarden);
            
            // Do the call and obtain the response and marshall the JSON object to a concrete model (for the first page)
            var apiReponse = await httpClient.GetFromJsonAsync<BovensteVerdieping.ApiResponse>(url);

            // Collect the house listings of the first page in a collection
            List<BovensteVerdieping.Object> houseListings = apiReponse.Objects;

            //Check if there are other house listings pages to be collected
            int totalPages = apiReponse.Paging.AantalPaginas;
            if ( totalPages > 1 ) {
                // if there are, get all the pages and add the house listings to the main collection
                for ( int i = 2; i <= totalPages; i++ ) {
                    url = getApiUrlWithParameters(withGarden, i);
                    apiReponse = await httpClient.GetFromJsonAsync<BovensteVerdieping.ApiResponse>(url);
                    houseListings.AddRange(apiReponse.Objects);
                }
            }

            return houseListings;
        }

        public async Task<List<BovensteVerdieping.Object>> GetHousesWithGarden()
        {   
            return await GetHouses(true);
        }

        // get the endpoint URL
        private string GetEndpointUrl() {
            string endpointUrl = ENPOINT_URL;
            string apiKey = appConfiguration.FundaApiKey;
            return endpointUrl.Replace("%%APIKEY%%",apiKey);
        }
    
        // create the complete URL
        private string getApiUrlWithParameters(bool withGarden = false, int page = 1) {
            // Obtain endpoint URL with API KEY
            string enpointUrl = GetEndpointUrl();
            // Create the complete URL with parameters
            var parameters = new Dictionary<string, string>()
                {
                    { "type", "koop" },
                    { "zo", SEARCH_VALUE_WITHOUT_GARDEN },
                    { "page", null },
                    { "pagesize", null },
                };
            // 
            if ( withGarden ) {
                parameters["zo"] = SEARCH_VALUE_WITH_GARDEN;
            }
            //  for this demo set an hard limit of 1000 listings on 1 page
            parameters["page"] = page+"";
            // parameters["pagesize"] = "1000"; <-- API DONT WORK WITH PAGINATION GREATER THAN 25
            parameters["pagesize"] = "25";
            // create the definitive URL
            var url = $"{enpointUrl}?{string.Join("&", parameters.Select(param => $"{param.Key}={param.Value}"))}";
            return url;
        }

    }

}