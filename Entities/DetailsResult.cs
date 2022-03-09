using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace WebApp


{
    public class DeatilsResult
    {
       public ICollection<Album> Albums { get; set; }
      public ICollection<Artist> Artists { get; set; }
      public ICollection<Track> Tracks { get; set; }
       
    }
}