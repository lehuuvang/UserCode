using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserCode.Models;

namespace UserCode.Pages.SignIn
{
    public class ResetPasswordModel : PageModel
    {
        private readonly Comic_Read_WebsiteContext _context;
        public ResetPasswordModel(Comic_Read_WebsiteContext context)
        {
            _context = context;
        }
        public Account account { get; set; }
        public async Task<IActionResult> OnPostAsync(string email, string password,string paswordConfirm)
        {

            var check = _context.Accounts.FirstOrDefault(p => p.Email == email);
            if(check != null)
            {
                check.PassWord = password.ToString();
                password = paswordConfirm;
                 _context.Accounts.AddAsync(account);
                 _context.SaveChangesAsync();
            }
            else
            {
                return Page();
            }
            return RedirectToPage("../SignIn/Login");
        }
        public void OnGet()
        {
        }
    }
}
