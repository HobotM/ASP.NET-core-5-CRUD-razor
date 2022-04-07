using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;



namespace Matt
{
    public class EditModel : PageModel
    {
        private readonly Entities.Chinook _context;

        public EditModel(Entities.Chinook context)
        {
            _context = context;
        }

        [BindProperty]
        public Album Album { get; set; }
        public Artist Artist { get; set; }



        public async Task<IActionResult> OnGetAsync(long? id)
        {
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "ArtistId", "Name"); // Created a ViewData of ArtistId wich is used in html and it contains Artist Id and Name

            // If if not found than redirect to NotFound page
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

            if (Album == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(long? id)
        {
            var albumToUpdate = await _context.Albums.FindAsync(id);

            if (albumToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Album>(
                albumToUpdate,
                "Album",
                s => s.Title, s => s.ArtistId))
            {
                //await _context.SaveChangesAsync();

                string[] Ids = Request.Form["item.TrackId"];
                long[] TrackIds = Array.ConvertAll(Ids, long.Parse);
                string[] TrackNames = Request.Form["item.Name"];
                

                for (int i = 0; i < TrackIds.Count(); i++)
                {
                    Track trackToUpdate = await _context.Tracks.FindAsync(TrackIds[i]);
                    if (await TryUpdateModelAsync<Track>(trackToUpdate))
                    {
                        trackToUpdate.Name = TrackNames[i];
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool AlbumExists(long? id)
        {
            return _context.Albums.Any(e => e.AlbumId == id);
        }
    }
}