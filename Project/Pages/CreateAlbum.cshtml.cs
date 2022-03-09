using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Entities;
using static System.Console;
using Microsoft.EntityFrameworkCore;


namespace WebApp.Pages
{
   public class CreateAlbumModel : PageModel
    {
        private readonly Entities.Chinook _context;

        public CreateAlbumModel(Entities.Chinook context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Album Album { get; set; }
        

        #region snippet_OnPostAsync
        public async Task<IActionResult> OnPostAsync()
        {
            #region snippet_TryUpdateModelAsync
            var emptyAlbum = new Album();

            if (await TryUpdateModelAsync<Album>(
                emptyAlbum,
                "Album",   // Prefix for form value.
                s=> s.Title, s => s.Artist, s => s.Tracks))
            {
                _context.Albums.Add(emptyAlbum);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            #endregion

            return Page();
        }
        #endregion
    }
}
