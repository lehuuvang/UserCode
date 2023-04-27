using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserCode.Models;
using UserCode.Pages.DetailComic;

namespace UserCode.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;
        public List<Comic> comics;
        public string accountname { get; set; }
        public IndexModel(ILogger<IndexModel> logger, Models.Comic_Read_WebsiteContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IList<Comic> comic { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.Comics != null)
            {
                comic = await _context.Comics.ToListAsync();

            }

            //accountname = HttpContext.Session.GetString("AccountName");
        }
        //public IActionResult OnGetSearch(string term)
        //{
        //    var names = _context.Comics.Where(p => p.ComicName.Contains(term)).Select(p => p.ComicName).ToList();
        //    return new JsonResult(names);
        //}    
        //public void OnGet()
        //{
        //    AddListComic listcomic = new AddListComic();
        //    comics = listcomic.findAll();
        //}
    }
}