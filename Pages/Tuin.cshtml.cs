using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RealEstateService = Services.RealEstateService;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace BovensteVerdieping.Pages
{
    public class TuinModel : PageModel
    {
        private readonly ILogger<TuinModel> _logger;
        private readonly IRealEstateService realEstateService;

        public TuinModel(ILogger<TuinModel> logger, IRealEstateService realEstateService)
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
            var topTenRealEstate = await realEstateService.GetTopRealEstatesWithGarden();
            return new JsonResult(topTenRealEstate);
        }
    }
}
