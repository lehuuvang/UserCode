using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserCode.Models;

namespace UserCode.Pages.commentuser
{
    public class IndexModel : PageModel
    {
        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;
       
        public IndexModel(UserCode.Models.Comic_Read_WebsiteContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<Comment> Comment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Comments != null)
            {
                Comment = await _context.Comments
                .Include(c => c.Account)
                .Include(c => c.Chapter).ToListAsync();
            }
        }
        public Comment Commenttt { get; set; } = new Comment();  

        public async Task<IActionResult> OnPostAsync(string commentText)
        {
           var accountExist = HttpContext.Session.GetInt32("AccountId");

          // var Commentt = _context.Comments.FirstOrDefault(p => p.AccountId.Equals(accountExist));
            if (HttpContext.Session.GetString("AccountName") != null)
            {            
                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }
                    Commenttt.CommnentContent = commentText;
                    Commenttt.Date = DateTime.Now;
                    await _context.Comments.AddAsync(Commenttt);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");             
            }
            else
            {
                return RedirectToPage("../SignIn/Login");
            }
            return Page();
        }
    }
}
