using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UserCode.Models;

namespace UserCode.Pages.SignUp
{
    public class IndexModel : PageModel
    {
        private readonly UserCode.Models.Comic_Read_WebsiteContext _context;

        public IndexModel(UserCode.Models.Comic_Read_WebsiteContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Accounts != null)
            {
                Account = await _context.Accounts
                .Include(a => a.CategoryAcc).ToListAsync();
                Account = await _context.Accounts.ToListAsync();
            }
        }
    }
}
