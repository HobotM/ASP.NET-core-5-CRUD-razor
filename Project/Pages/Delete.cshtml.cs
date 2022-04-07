
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Entities;
using System.Collections.Generic;
using System.Linq;


namespace Matt
{
    public class DeleteModel : PageModel
    {
        private readonly Entities.Chinook _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(Entities.Chinook context,
                           ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Album Album { get; set; }
        public ICollection<Track> Tracks { get; set; }
        public string ErrorMessage { get; set; }

        // on get method reads Album id and all the tracks allocated to that album
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // includes tracks, artist 
            Album = await _context.Albums
            .Include(a => a.Tracks)
            .Include(a => a.Artist)
            .AsNoTracking() //entities returned will not be cached
            .FirstOrDefaultAsync(m => m.AlbumId == id); //Asynchronously returns the first element of a sequence in this case ID, or a default value if the sequence contains no elements.
            // if no album then reurn to index
            if (Album == null)
            {
                return NotFound();
            }
            return Page();
        }

        // on post method 
        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Insctance fo album includes tracks, artist 
            Album = await _context.Albums
            .Include(a => a.Tracks)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.AlbumId == id);


            // if album is true than save changes 
            if (Album != null)
            {


                _context.Albums.Remove(Album);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
