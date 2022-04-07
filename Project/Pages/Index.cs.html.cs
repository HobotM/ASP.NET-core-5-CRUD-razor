using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Matt;

namespace Matt
{
    public class IndexModel : PageModel
    {
        private readonly Entities.Chinook _context;
        private readonly IConfiguration Configuration;

        public IndexModel(Entities.Chinook context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string NameSort1 { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Album> Albums { get; set; } // created class  PaginatedList<T> was made (Microsoft documentation)
        
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            // assign strings to suit switch statement and pass sortOrder parameter
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            NameSort1 = String.IsNullOrEmpty(sortOrder) ? "name_desc1" : "";
           
            
            // if no search string than start on page 1
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Album> albumsId = from s in _context.Albums
                                             select s;
            
            // if statement that is used for searching function using artist name or title
            if (!String.IsNullOrEmpty(searchString))
            {
                albumsId = albumsId.Where(s => s.Artist.Name.Contains(searchString)
                                       || s.Title.Contains(searchString));
            }
            // switch statement to change the sorting - can be by artist name or album name
            switch (sortOrder)
            {
                case "name_desc":
                    albumsId = albumsId.OrderByDescending(s => s.Artist.Name);
                    break;
                case "name_desc1":
                    albumsId = albumsId.OrderByDescending(s => s.Title);
                    break;
                
                default:
                    albumsId = albumsId.OrderBy(s => s.Artist.Name);
                    break;
            }
            //Create an instance of Album with the paginating option including artist table 
            var pageSize = Configuration.GetValue("PageSize", 4);
            Albums = await PaginatedList<Album>.CreateAsync(
            albumsId.AsNoTracking().Include(a => a.Artist), pageIndex ?? 1, pageSize);
        }
    }
}