using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

using Entities;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IList<QueryResult> queryResults { get; set; }
        public IEnumerable<Album> Albums { get; set; }


        public string SearchTerm { get; set; }
        

        


        public void OnGet(string SearchTerm)
        {
            ViewData["StudentInfo"] = "Mateusz Hobot (B00412541)";
            Chinook db = new Chinook();

            if (string.IsNullOrEmpty(SearchTerm))
            {
                queryResults = db.Albums.Join(
                db.Artists, alb => alb.ArtistId, art => art.ArtistId, (alb, art) => new QueryResult()
                {
                    AlbumId = alb.AlbumId,
                    Title = alb.Title,
                    ArtistName = art.Name
                }
            ).OrderBy(Artist => Artist.ArtistName).ToList();
            }
            else
            {
                queryResults = db.Albums.Join(
                db.Artists, alb => alb.ArtistId, art => art.ArtistId, (alb, art) => new QueryResult()
                {
                    AlbumId = alb.AlbumId,
                    Title = alb.Title,
                    ArtistName = art.Name
                }
            ).OrderBy(Artist => Artist.ArtistName).Where(e => e.Title.Contains(SearchTerm)).ToList();
            }

        }

    }


}

