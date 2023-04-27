using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserCode.Models;

namespace UserCode.ViewComponents
{
    [ViewComponent(Name = "CategoryComicView")]
    public class Categorycomic : ViewComponent
    {
        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;
        public Categorycomic( Models.Comic_Read_WebsiteContext context)
        {
            _context = context;
        }
        public IList<Category> categorycomic { get; set; } = default!;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_context.Categories != null)
            {
                categorycomic = await _context.Categories.ToListAsync();
            }   
            return View(categorycomic);
        }
    }
}
