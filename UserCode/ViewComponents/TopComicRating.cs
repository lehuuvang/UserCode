using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using UserCode.Models;

namespace UserCode.ViewComponents
{
    [ViewComponent(Name = "TopComicRatingView")]
    public class TopComicRating :ViewComponent
    {
        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;
        public TopComicRating(Models.Comic_Read_WebsiteContext context)
        {
            _context = context;
        }
        public IList<Comic> comic { get; set; } = default!;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_context.Comics != null)
            {
                if (_context.Comics.Where(c => c.Rating == 10) != null)
                {
                    comic = await _context.Comics.ToListAsync();
                }
            }
            return View(comic);
        }
    }
}
