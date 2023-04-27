using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserCode.Models;
using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;
using System;

namespace UserCode.Pages.SignIn
{
    public class forgotPasswordModel : PageModel
    {
        private readonly Comic_Read_WebsiteContext _Context;
        int OTP;
        public forgotPasswordModel(Comic_Read_WebsiteContext context)
        {
            _Context = context;
        }
        public Account account { get; set; }
        Random random = new Random();
        public async Task<IActionResult> OnPostAsync(string email)
        {
            OTP = random.Next(100000, 1000000);
            var check = _Context.Accounts.FirstOrDefault(p => p.Email == email);
            if (email == null) { ViewData["checkmail"] = "bạn chưa nhập email"; }
            if (check == null) { ViewData["checkmail"] = "email không tồn tại"; }
            if (check != null)
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(email.Trim());
                mailMessage.From = new MailAddress("lehuuvang1@gmail.com");
                mailMessage.Subject = "Yêu cầu mật khẩu";
                mailMessage.Body = "Mã xác nhận mật khẩu của bạn là:" + OTP.ToString();
                mailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential("lehuuvang1@gmail.com", "ueriwsnrsvkzyqlr");
                smtp.Send(mailMessage);          
                HttpContext.Session.SetString("OTP", OTP.ToString());
                return RedirectToPage("../SignIn/codeSubmit");
            }
            return RedirectToPage("../SignIn/forgotPassword");
        }
        public bool check1(string otp)
        {
            if (OTP.ToString().Trim().Equals(otp))
            {
                return true;
            }
            return false;
        }      
    }

}

