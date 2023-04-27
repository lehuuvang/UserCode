using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserCode.Models;
namespace UserCode.Pages.DetailComic
{
    public class IndexModel : PageModel
    {
        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;

        public IndexModel(UserCode.Models.Comic_Read_WebsiteContext context)
        {
            _context = context;
        }

        public Comic Comic { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Comics == null)
            {
                return NotFound();
            }

            var comic = await _context.Comics.FirstOrDefaultAsync(m => m.ComicId == id);
            if (comic == null)
            {
                return NotFound();
            }
            else
            {
                Comic = comic;
            }
            return Page();
        }
        public Chapter Chapter { get; set; }
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || _context.Chapters == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapters.FirstOrDefaultAsync(m => m.ChapterId == id);
            if (chapter == null)
            {
                return NotFound();
            }
            else
            {
                Chapter = chapter;
            }
            return Page();
        }
    }
}
