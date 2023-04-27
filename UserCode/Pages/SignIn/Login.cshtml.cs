using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Session;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NToastNotify;
using UserCode.Models;
using UserCode.Pages.Options;

namespace UserCode.Pages
{
    public class LoginModel : PageModel
    {
        // đổ thuộc tính trục tiếp từ input lên.
        [BindProperty]
        public Account account { get; set; }
        private Comic_Read_WebsiteContext _context;
        private readonly IToastNotification _notify;
        private readonly RecaptchaOption _option;
        public string msg;
        public LoginModel(Comic_Read_WebsiteContext context,IToastNotification notify,IOptions<RecaptchaOption> option)
        {
            _context = context;
            this._notify = notify;
            _option = option.Value;
        }
        public void OnGet()
        {
            account = new Account();      
        }
        public IActionResult OnPost()
        {            
            var model = new CaptchaViewModel()
            {
                SiteKey = _option.SiteKey
            };
            if (HttpContext.Session.GetString("AccountName") != null)
            {
                HttpContext.Session.Remove("AccountName");
                return Page();
            }
            var acc = Login(account.AccountName, account.PassWord);         
            if (acc == null)
            {
                _notify.AddWarningToastMessage("Mật khẩu tài khoản không đúng!");
                return Page();
            }
            else
            {
               // HttpContext.Session.SetInt32("AccountId", acc.AccountId);
                HttpContext.Session.SetString("AccountName", acc.AccountName);
                _notify.AddSuccessToastMessage("Đăng nhập thành công");
                return RedirectToPage("../Index");
                ///25:22               
            }
        }
        public Account Login(string accountName, string password)
        {
            try
            {
                    
                var account = _context.Accounts.FirstOrDefault(a => a.AccountName.Equals(accountName) && a.PassWord.Equals(password));
                if (account != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, account.PassWord))
                    {
                        return account;
                    }
                }
                    return null;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return account;
            }
        }
    }
}
