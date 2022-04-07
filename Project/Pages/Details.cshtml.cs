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
    public class DetailsModel : PageModel
    {
      private readonly Entities.Chinook _context;

        public DetailsModel(Entities.Chinook context)
        {
            _context = context;
        }

        public Album Album  { get; set; }

        // Get the albumId and include Artist and Tracks
        public async Task<IActionResult> OnGetAsync(long? id)
        {   // if there is no id then return not found 
            if (id == null)
            {
                return NotFound();
            }
            // includes artist, tracks for selected album id
            Album = await _context.Albums
                .Include(a => a.ArtistId)
                .Include(a => a.Tracks)        
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AlbumId == id);

            if (Album == null)
            {
                return NotFound();
            }
            return Page();
        }

    }

}
