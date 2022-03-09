
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Entities;

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
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Tracks) 
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.AlbumId == id);

            if (Album == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again", id);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albums = await _context.Albums.FindAsync(id);

            if (albums == null)
            {
                return NotFound();
            }

            try
            {
                _context.Albums.Remove(albums);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, ErrorMessage);

                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}
