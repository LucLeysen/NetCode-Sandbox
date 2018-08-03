using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RazorPages.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CreateModel> _log;

        public CreateModel(AppDbContext db, ILogger<CreateModel> log)
        {
            _db = db;
            _log = log;
        }

        [TempData]
        public string Message
        {
            get; set;
        }

        [BindProperty]
        public Customer Customer
        {
            get; set;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();
            var message = $"Custumer {Customer.Name} added!";
            Message = message;
            _log.LogCritical(message);
            return RedirectToPage("/Index");
        }
    }
}