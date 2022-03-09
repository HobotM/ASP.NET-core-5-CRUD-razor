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


        public async Task<IActionResult> OnGetAsync(int? id)
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
            return Page();
        }


        // public async Task OnGet(int AlbumId)
        // {
        //     ViewData["Info"] = "Album";

        //     using (Chinook db = new Chinook())
        //     {
        //         // Heading = "Album";
        //         // var queryResults = db.Albums
        //         // .Join(
        //         //     db.Artists,
        //         //     alb => alb.AlbumId,
        //         //     art => art.ArtistId,
        //         //     (alb, art) => new
        //         //     {
        //         //         AlbumId = alb.AlbumId,
        //         //         AlbumName = alb.Title,
        //         //         ArtistName = art.Name
        //         //     }
        //         // ).ToList();
        //         // (
        //         //     db.Tracks,
        //         //     alb => alb.AlbumId,
        //         //     track => track.TrackId,
        //         //     (alb, track) => new
        //         //     {
        //         //         AlbumId = alb.AlbumId,
        //         //         AlbumName = alb.AlbumName,
        //         //         ArtistName = alb.ArtistName,
        //         //         TrackName = track.Name
        //         //     }
        //         // ).ToList();


        //     }
        // }


    }

}