﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using S3_Security_System.Data;
using S3_Security_System.Models;

namespace S3_Security_System.Pages.EntranceTokens
{
    public class DeleteModel : PageModel
    {
        private readonly S3_Security_System.Data.S3_Security_SystemContext _context;

        public DeleteModel(S3_Security_System.Data.S3_Security_SystemContext context)
        {
            _context = context;
        }

        [BindProperty]
      public EntranceToken EntranceToken { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EntranceToken == null)
            {
                return NotFound();
            }

            var entrancetoken = await _context.EntranceToken.FirstOrDefaultAsync(m => m.ID == id);

            if (entrancetoken == null)
            {
                return NotFound();
            }
            else 
            {
                EntranceToken = entrancetoken;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.EntranceToken == null)
            {
                return NotFound();
            }
            var entrancetoken = await _context.EntranceToken.FindAsync(id);

            if (entrancetoken != null)
            {
                EntranceToken = entrancetoken;
                _context.EntranceToken.Remove(EntranceToken);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
