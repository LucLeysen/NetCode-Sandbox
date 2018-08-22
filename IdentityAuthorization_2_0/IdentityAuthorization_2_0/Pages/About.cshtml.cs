using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityAuthorization_2_0.Pages
{
    [Authorize(Policy = "BelgiumOrAdmin")]
    public class AboutModel : PageModel
    {
        public string Message
        {
            get; set;
        }

        public void OnGet()
        {
            Message = "Your application description page.";
        }
    }
}
