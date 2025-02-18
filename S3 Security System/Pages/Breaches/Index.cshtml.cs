﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using S3_Security_System.Data;
using S3_Security_System.Models;

namespace S3_Security_System.Pages.Breaches
{
    public class IndexModel : PageModel
    {
        private readonly S3_Security_System.Data.S3_Security_SystemContext _context;

        public IndexModel(S3_Security_System.Data.S3_Security_SystemContext context)
        {
            _context = context;
        }

        public IList<Breach> Breach { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Breach != null)
            {
                Breach = await _context.Breach
                .Include(b => b.BreachType)
                .Include(b => b.S3_Security_SystemUser).ToListAsync();
            }
        }
    }
}
