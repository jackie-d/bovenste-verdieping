using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RealEstateService = Services.RealEstateService;

namespace BovensteVerdieping.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly RealEstateService realEstateService;
        public List<KeyValuePair<int, Services.RealEstate>> topTenRealEstate;

        public IndexModel(ILogger<IndexModel> logger, RealEstateService realEstateService)
        {
            _logger = logger;
            this.realEstateService = realEstateService;  
        }

        public async void OnGet()
        {
            this.topTenRealEstate = await realEstateService.GetTopRealEstates();
            foreach ( var realEstate in topTenRealEstate ) {
                Console.WriteLine(realEstate.Value.name);
            }
        }
    }
}
