using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserCode.Models;

namespace UserCode.Pages.SearchComics
{
    public class comicSearchModel : PageModel
    {

        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;
        public List<Comic> comics;
        public string accountname { get; set; }
        public comicSearchModel(Models.Comic_Read_WebsiteContext context)
        {
            _context = context;
        }
        public IList<Comic> comic { get; set; } = default!;
        public async Task OnGetAsync(string term)
        {
            var query = await _context.Comics.ToListAsync();
            if (!String.IsNullOrEmpty(term))
            {
                comic = query.Where(s => s.ComicName.Contains(term)).ToList();
                ViewData["search"] = term;
            }
            else
            {
                comic = await _context.Comics.ToListAsync();
            }
        }
    }
}
