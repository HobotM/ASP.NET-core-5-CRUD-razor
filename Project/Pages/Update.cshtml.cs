using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;
using Matt;



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
            ViewData["ArtistId"] = new SelectList(_context.Set<Artist>(), "ArtistId", "Name");

            if (id == null)
            {
                return NotFound();
            }

            // Album = await _context.Albums.FindAsync(id);
            Album = await _context.Albums
            .Include(a => a.Artist)
            .Include(a => a.Tracks)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.AlbumId == id);

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
                "album",
                s => s.Title, s => s.AlbumId))
            {
                await _context.SaveChangesAsync();

                string[] Ids = Request.Form["item.TrackId"];
                int[] TrackIds = Array.ConvertAll(Ids, int.Parse);
                string[] TrackNames = Request.Form["item.Name"];
                // int trackCount = TrackIds.Count();

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