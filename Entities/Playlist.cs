using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entities
{
    [Table("playlists")]
    public partial class Playlist
    {
        public Playlist()
        {
            PlaylistTracks = new HashSet<PlaylistTrack>();
        }

        [Key]
        public long PlaylistId { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string Name { get; set; }

        [InverseProperty(nameof(PlaylistTrack.Playlist))]
        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }
}
