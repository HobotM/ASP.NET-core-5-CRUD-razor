using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using Entities;

namespace WebApp.Pages
{
    public class CreateAlbumModel : PageModel
    {
        public IList<Artist> artists { get; set; }
        public IList<Album> albums { get; set; }
        public IList<Track> tracks { get; set; }

        public void OnGet()
        {

            Chinook context = new Chinook();
            artists = context.Artists.ToList();
        }
        public IActionResult OnPost()
        {
            Chinook context = new Chinook();
            string title = Request.Form["titlename"];
            int arts = int.Parse(Request.Form["artist"]);


            Album newAlbum = new Album
            {
                Title = title,
                ArtistId = arts
            };
            context.Albums.Add(newAlbum);
            context.SaveChanges();

            albums = context.Albums.ToList();

            // Get new Album Id
            long? newalbId = newAlbum.AlbumId;

            // Get all the input on Track array
            string[] trackList = Request.Form["song[]"];

            // Insert all input on Track array into Track database
            foreach (var trk in trackList)
            {
                Track newTrack = new Track();
                newTrack.AlbumId = newalbId;
                newTrack.Name = trk;
                // Default value set for below, as its currently not being ask in insert form
                newTrack.GenreId = 3;
                newTrack.MediaTypeId = 3;
                newTrack.Milliseconds = 375418;
                newTrack.UnitPrice = 1;
                context.Tracks.AddAsync(newTrack);
                context.SaveChangesAsync();
            }
            return Redirect("~/Index");

        }
    }
}