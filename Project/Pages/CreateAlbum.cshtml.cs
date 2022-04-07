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

        // Get the list of artist from the artist context
        public void OnGet()
        {
            // create instance of the Chinook and add artist from that context to list so we can use it in the main page
            Chinook context = new Chinook();
            artists = context.Artists.ToList();
        }
        public IActionResult OnPost()
        {
            Chinook context = new Chinook(); // create instance of the Chinook
            string title = Request.Form["titlename"]; // create form data titlename for string title
            int arts = int.Parse(Request.Form["artist"]); // create form data artist for int arts

            //create instance of the album
            Album newAlbum = new Album
            {
                Title = title,
                ArtistId = arts
            };
            context.Albums.Add(newAlbum);
            context.SaveChanges();

            albums = context.Albums.ToList();

            // Generate new album id
            long? newalbId = newAlbum.AlbumId;

            // Get all the input on Track array
            string[] trackList = Request.Form["song[]"];

            // Insert all input on Track array into Track database
            foreach (var trk in trackList)
            {
                Track newTrack = new Track();
                newTrack.AlbumId = newalbId;
                newTrack.Name = trk;
                // Default value set for below, not used 
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