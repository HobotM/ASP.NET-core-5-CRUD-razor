using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entities
{
    [Table("artists")]
    public partial class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        [Key]
        public long? ArtistId { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string Name { get; set; }

        [InverseProperty(nameof(Album.Artist))]
        public virtual ICollection<Album> Albums { get; set; }
    }
}
