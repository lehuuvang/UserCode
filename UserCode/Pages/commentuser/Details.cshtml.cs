using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserCode.Models;

namespace UserCode.Pages.commentuser
{
    public class DetailsModel : PageModel
    {
        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;

        public DetailsModel(UserCode.Models.Comic_Read_WebsiteContext context)
        {
            _context = context;
        }

      public Comment Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FirstOrDefaultAsync(m => m.CommnentId == id);
            if (comment == null)
            {
                return NotFound();
            }
            else 
            {
                Comment = comment;
            }
            return Page();
        }
    }
}
