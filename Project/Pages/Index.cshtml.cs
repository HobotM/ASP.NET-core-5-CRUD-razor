using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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

        public void OnGet()
        {
            ViewData["StudentInfo"] = "Mateusz Hobot (B00412541)";
            
            Chinook db = new Chinook();
            queryResults = db.Albums.Join(
                db.Artists, alb => alb.ArtistId, art => art.ArtistId, (alb, art) => new QueryResult()
                {
                    AlbumId = alb.AlbumId,
                    Title = alb.Title,
                    ArtistName = art.Name
                    
                    
                }
            ).OrderBy(Artist => Artist.ArtistName).ToList();
        }
    }
}
