
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

             public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Album = await _context.Albums
            .Include(a => a.Tracks)
            .FirstOrDefaultAsync(m => m.AlbumId == id);

            if (Album == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
                Album = await _context.Albums
                .Include(a => a.Tracks)  
                        
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AlbumId == id);

            if (Album != null)
            {

                 
                _context.Albums.Remove(Album);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
