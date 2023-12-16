using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BovensteVerdieping.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BovensteVerdieping.Models;
using System;
using System.Collections.Generic;

namespace BovensteVerdieping.Pages
{
    public class TuinModel : PageModel
    {
        private readonly ILogger<TuinModel> _logger;
        private readonly IRealEstateService realEstateService;
        private readonly SampleDataService sampleDataService;

        public TuinModel(ILogger<TuinModel> logger, IRealEstateService realEstateService, SampleDataService sampleDataService)
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
                topTenRealEstate = await realEstateService.GetTopRealEstatesWithGarden();   
            } else {
                topTenRealEstate = sampleDataService.GetRealEstates();
            }
            return new JsonResult(topTenRealEstate);
        }
    }
}
