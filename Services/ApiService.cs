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

        private readonly string EndpointUrl = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json/%%APIKEY%%/";
        
        private readonly string SearchValueWithoutGarden = "/amsterdam/tuin/";
        private readonly string SearchValueWithGarden = "/amsterdam/";
        
        private AppConfiguration appConfiguration;
        private HttpClient httpClient;

        public ApiService(AppConfiguration appConfiguration) {
            httpClient = new HttpClient();
            this.appConfiguration = appConfiguration;
        }

        // public accessible methods to retrieve API data

        public async Task<string> GetHouses(bool withGarden = false)
        {
           string enpointUrl = GetEndpointUrl();
           var parameters = new Dictionary<string, string>()
                {
                    { "type", "koop" },
                    { "zo", SearchValueWithoutGarden },
                    { "page", null },
                    { "p", "" },
                };
            if ( withGarden ) {
                parameters["zo"] = SearchValueWithGarden;
            }
            parameters["page"] = "1";
            var url = $"{enpointUrl}?{string.Join("&", parameters.Select(param => $"{param.Key}={param.Value}"))}";
            
            Console.WriteLine("url " + url);

            var apiReponse = await httpClient.GetFromJsonAsync<BovensteVerdieping.ApiResponse>(url);

            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(apiReponse))
            {
                string name=descriptor.Name;
                object value=descriptor.GetValue(apiReponse);
                Console.WriteLine("{0}={1}",name,value);
            }

            return null;
        }

        public async Task<string> GetHousesWithGarden()
        {   
            await GetHouses(true);
            return null;
        }

        // get the endpoint URL
        private string GetEndpointUrl() {
            string endpointUrl = EndpointUrl;
            string apiKey = appConfiguration.FundaApiKey;
            return endpointUrl.Replace("%%APIKEY%%",apiKey);
        }

    }

}