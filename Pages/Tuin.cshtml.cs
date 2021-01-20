using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RealEstateService = Services.RealEstateService;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BovensteVerdieping.Pages
{
    public class TuinModel : PageModel
    {
        private readonly ILogger<TuinModel> _logger;
        private readonly RealEstateService realEstateService;

        public TuinModel(ILogger<TuinModel> logger, RealEstateService realEstateService)
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
            var topTenRealEstate = await realEstateService.GetTopRealEstatesWithGarden();
            return new JsonResult(topTenRealEstate);
        }
    }
}
