using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entities
{
    [Table("genres")]
    public partial class Genre
    {
        public Genre()
        {
            Tracks = new HashSet<Track>();
        }

        [Key]
        public long GenreId { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string Name { get; set; }

        [InverseProperty(nameof(Track.Genre))]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
