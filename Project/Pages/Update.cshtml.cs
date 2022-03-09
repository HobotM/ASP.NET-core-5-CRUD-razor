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
    public class UpdateModel : PageModel
    {
        private readonly ILogger<UpdateModel> _logger;

        public UpdateModel(ILogger<UpdateModel> logger)
        {
            _logger = logger;
        }
        
        



        public void OnGet(int AlbumId)
        {
            ViewData["StudentInfo"] = "Mateusz Hobot (B00412541)";
            
            Chinook db = new Chinook();
            var queryResults = db.Albums.Join(
                db.Artists, alb => alb.ArtistId, art => art.ArtistId, (alb, art) => new QueryResult()
                {
                    Title = alb.Title,
                    ArtistName = art.Name
                    
                    
                }
            ).ToList();
        }
    }
}
