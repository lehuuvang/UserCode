using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserCode.Pages.Shared
{
    public class _loginPatialviewModel : PageModel
    {
        public string accountname { get; set; }
        public void OnGet()
        {
            accountname = HttpContext.Session.GetString("AccountName");
        }
        public IActionResult OnGetLogout()
        {
            if (HttpContext.Session.GetString("AccountName") != null)
            {
                HttpContext.Session.Remove("AccountName");
                return RedirectToPage("../SignIn/Login");
            }
            return Page();
        }
    }
}
