﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using S3_Security_System.Areas.Identity.Data;
using S3_Security_System.Data;
using S3_Security_System.Models;

namespace S3_Security_System.Pages.EntranceTokens
{
    public class EditModel : PageModel
    {
        private readonly UserManager<S3_Security_SystemUser> _userManager;
        private readonly SignInManager<S3_Security_SystemUser> _signInManager;
        private readonly S3_Security_System.Data.S3_Security_SystemContext _context;

        public EditModel(S3_Security_System.Data.S3_Security_SystemContext context, UserManager<S3_Security_SystemUser> userManager,
            SignInManager<S3_Security_SystemUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public EntranceToken EntranceToken { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.EntranceToken == null)
            {
                return NotFound();
            }

            var entrancetoken =  await _context.EntranceToken.FirstOrDefaultAsync(m => m.ID == id);
            if (entrancetoken == null)
            {
                return NotFound();
            }
            EntranceToken = entrancetoken;
           ViewData["S3_Security_SystemUserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            EntranceToken.S3_Security_SystemUserId = _userManager.GetUserId(User);
            EntranceToken.S3_Security_SystemUser = await _userManager.GetUserAsync(User);
            EntranceToken.TimeOfExit= DateTime.Now;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(EntranceToken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntranceTokenExists(EntranceToken.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Index");
        }

        private bool EntranceTokenExists(int id)
        {
          return (_context.EntranceToken?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
