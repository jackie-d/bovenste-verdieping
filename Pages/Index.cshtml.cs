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

        public IndexModel(ILogger<IndexModel> logger, RealEstateService realEstateService)
        {
            _logger = logger;
            this.realEstateService = realEstateService;  
        }

        public void OnGet()
        {
            //
        }

        public async Task<JsonResult> OnGetGetTopRealEstates(string name)
        {
            var topTenRealEstate = await realEstateService.GetTopRealEstates();
            foreach ( var realEstate in topTenRealEstate ) {
                Console.WriteLine(realEstate.Value.name);
            }
            return new JsonResult(topTenRealEstate);
        }

    }
}
