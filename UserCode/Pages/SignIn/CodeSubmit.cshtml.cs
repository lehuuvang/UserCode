using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserCode.Pages.SignIn
{
    public class CodeSubmitModel : PageModel
    {
        public string otpcheck { get; set; }
        public async Task<IActionResult> OnGetAsync(string otpcheckview)
        {
            otpcheck = HttpContext.Session.GetString("OTP");
            if(otpcheck != null)
            {
                if (otpcheck.Equals(otpcheckview) == true)
                {
                    return RedirectToPage("../SignIn/ResetPassword");
                }else
                {
                    ViewData["checkotp"] = "Ma xac nhan sai!!";
                }
            }
            return Page();
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("OTP");
            return RedirectToPage("../SignIn/Login");
        }
    }
}
