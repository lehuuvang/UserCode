using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using UserCode.Models;

namespace UserCode.Pages.SignUp
{
    public class CreateModel : PageModel
    {
        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;
        private readonly IToastNotification _notify;

        public CreateModel(UserCode.Models.Comic_Read_WebsiteContext context, IToastNotification notify)
        {
            _context = context;
            this._notify = notify;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryAccName"] =new  SelectList(_context.CategoryAccounts, "CategoryAccId", "CategoryAccName");
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var checkmail = _context.Accounts.FirstOrDefault(p => p.Email.Equals(Account.Email));
            var checksdt = _context.Accounts.FirstOrDefault(p => p.Phone.Equals(Account.Phone));
            if (!ModelState.IsValid)
            {
                _notify.AddWarningToastMessage("Lỗi hệ thống!");
                return Page();
            }
            if (Account.AccountName == null)
            {
                _notify.AddWarningToastMessage("Tài khoản không được để trống!");
                return RedirectToPage("Create");
            }
            if(Account.Email == null)
            {
                _notify.AddWarningToastMessage("Email không được để trống!");
                return RedirectToPage("Create");
            }
            if(checkmail != null)
            {
                _notify.AddWarningToastMessage("Email đăng kí đã tồn tại");
                return RedirectToPage("Create");
            }
            if(checksdt != null)
            {
                _notify.AddWarningToastMessage("số điện thoại đã tồn tại");
                return RedirectToPage("Create");
            }
            if (Account.PassWord == null)
            {
                _notify.AddWarningToastMessage("Password không được để trống");
                return RedirectToPage("Create");
            }
            if (Account.Phone==null)
            {
                _notify.AddWarningToastMessage("Số điện thoại không được để trống");
                return RedirectToPage("Create");
            }

            //Account.PassWord = Console.ReadLine();
            //// generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            //byte[] salt = new byte[128 / 8];
            //using (var rngCsp = new RNGCryptoServiceProvider())
            //{
            //    rngCsp.GetNonZeroBytes(salt);
            //}
            //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
            //// derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            //string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            //    password: Account.PassWord,
            //    salt: salt,
            //    prf: KeyDerivationPrf.HMACSHA256,
            //    iterationCount: 100000,
            //    numBytesRequested: 256 / 8));
            //Console.WriteLine($"Hashed: {hashed}");
            ////Account.PassWord = HashCryp.HashPassword(Account.PassWord);

            _context.Accounts.Add(Account);
            await _context.SaveChangesAsync();
            _notify.AddSuccessToastMessage("Tạo tài khoản thành công!");
            return RedirectToPage("../SignIn/Login");  
        }
    }
}
