using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Data;
using System.Security.Principal;
using UserCode.Models;

namespace UserCode.Pages.DetailComic
{
    public class cartTextModel : PageModel
    {
        public static List<Comic> comics  = new List<Comic>();
        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;
        private readonly IToastNotification _notify;
        public cartTextModel(UserCode.Models.Comic_Read_WebsiteContext context, IToastNotification notify)
        {
            _context = context;
            this._notify = notify;
        }
        public IActionResult OnGetBuy(Guid id)
        {
            var check = _context.Comics.FirstOrDefault(m => m.ComicId == id);
            if (check != null)
            {
                //static
                //if (comics.Contains(check) == false)
                //{
                //    comics.Add(check);
                //    ViewData["comics"] = comics;
                //    return Page();
                //}
                //cookie
                string comicsId;
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                if (Request.Cookies["comics"] == null)
                {
                    comicsId = "a"+" "+ check.ComicId.ToString();
                }
                else
                {

                    if (Request.Cookies["comics"].Contains(check.ComicId.ToString()) == false)
                    {    
                        comicsId = Request.Cookies["comics"] + " " + check.ComicId.ToString();
                    }
                    else
                    {
                        _notify.AddErrorToastMessage("Truyện này đã được theo dõi!");
                        comicsId = Request.Cookies["comics"];
                        return RedirectToPage("../DetailComic/Index", new { id });
                    }
                }
                Response.Cookies.Append("comics", comicsId, cookieOptions);
                _notify.AddSuccessToastMessage("Theo dõi truyện thành công");
                return RedirectToPage("../DetailComic/Index", new { id});
            }
            return RedirectToPage("Index");
        }

        public IActionResult OnGetDelete(Guid id)
        {
            var check = _context.Comics.FirstOrDefault(m => m.ComicId == id);
            if (check != null)
            {
                string comicsId;
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(-1);
                if (Request.Cookies["comics"] == null)
                {
                    comicsId = "a" + " " + check.ComicId.ToString();
                }
                else
                {

                    if (Request.Cookies["comics"].Contains(check.ComicId.ToString()) == false)
                    {
                        comicsId = Request.Cookies["comics"] + " " + check.ComicId.ToString();
                    }
                    else
                    {
                        comicsId = Request.Cookies["comics"];
                    }
                }
                Response.Cookies.Delete("comics");
                return RedirectToPage("cartText");
            }
           return RedirectToPage("cartText");
        }
    }
}
