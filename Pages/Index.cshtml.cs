using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BovensteVerdieping.Services;
using BovensteVerdieping.Models;

namespace BovensteVerdieping.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRealEstateService realEstateService;
        private readonly SampleDataService sampleDataService;

        public IndexModel(ILogger<IndexModel> logger, IRealEstateService realEstateService, SampleDataService sampleDataService)
        {
            _logger = logger;
            this.realEstateService = realEstateService;  
            this.sampleDataService = sampleDataService;
        }

        public void OnGet()
        {
            //
        }

        public async Task<JsonResult> OnGetGetTopRealEstates(bool isSampleData = false)
        {
            List<KeyValuePair<int, RealEstate>> topTenRealEstate;
            if ( !isSampleData ) {
                topTenRealEstate = await realEstateService.GetTopRealEstates();   
            } else {
                topTenRealEstate = sampleDataService.GetRealEstates();
            }
            return new JsonResult(topTenRealEstate);
        }

    }
}
