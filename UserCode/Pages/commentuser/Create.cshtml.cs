using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserCode.Models;

namespace UserCode.Pages.commentuser
{
    public class CreateModel : PageModel
    {
        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;

        public CreateModel(UserCode.Models.Comic_Read_WebsiteContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AccountName"] = new SelectList(_context.Accounts, "AccountId", "AccountName");
        ViewData["ChapterName"] = new SelectList(_context.Chapters, "ChapterId", "ChapterName");
            return Page();
        }

        [BindProperty]
        public Comment Comment { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
