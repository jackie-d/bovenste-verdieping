using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BovensteVerdieping.Pages
{
    public class TuinModel : PageModel
    {
        private readonly ILogger<TuinModel> _logger;

        public TuinModel(ILogger<TuinModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
