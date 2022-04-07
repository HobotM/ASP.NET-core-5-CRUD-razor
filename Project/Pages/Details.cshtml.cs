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
                .Include(a => a.Artist)
                .Include(a => a.Tracks)        
                .AsNoTracking() //entities returned will not be cached
                .FirstOrDefaultAsync(m => m.AlbumId == id); //Asynchronously returns the first element of a sequence in this case ID, or a default value if the sequence contains no elements.
            // if no album then reurn to index
            if (Album == null)
            {
                return NotFound();
            }
            return Page();
        }

    }

}
