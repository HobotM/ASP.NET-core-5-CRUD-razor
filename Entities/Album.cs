using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    [Table("albums")]
    [Index(nameof(ArtistId), Name = "IFK_AlbumArtistId")]
    public partial class Album
    {
        public Album()
        {
            Tracks = new HashSet<Track>();
        }

        [Key]
        public long? AlbumId { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(160)")]
        public string Title { get; set; }
        public long? ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        [InverseProperty("Albums")]
        public virtual Artist Artist { get; set; }
        [InverseProperty(nameof(Track.Album))]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
