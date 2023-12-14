using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Services;

namespace BovensteVerdieping.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRealEstateService realEstateService;

        public IndexModel(ILogger<IndexModel> logger, IRealEstateService realEstateService)
        {
            _logger = logger;
            this.realEstateService = realEstateService;  
        }

        public void OnGet()
        {
            //
        }

        public async Task<JsonResult> OnGetGetTopRealEstates()
        {
            var topTenRealEstate = await realEstateService.GetTopRealEstates();
            return new JsonResult(topTenRealEstate);
        }

    }
}
