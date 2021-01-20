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
            parameters["page"] = "1";
            // parameters["pagesize"] = "1000"; <-- API DONT WORK WITH PAGINATION GREATER THAN 25
            parameters["pagesize"] = "25";
            // create the definitive URL
            var url = $"{enpointUrl}?{string.Join("&", parameters.Select(param => $"{param.Key}={param.Value}"))}";
            
            // Do the call and obtain the response and marshall the JSON object to a concrete model
            var apiReponse = await httpClient.GetFromJsonAsync<BovensteVerdieping.ApiResponse>(url);

            Console.WriteLine("url " + url);
            Console.WriteLine(apiReponse.TotaalAantalObjecten);
            Console.WriteLine(apiReponse.Objects.Count);

            return apiReponse.Objects;
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

    }

}