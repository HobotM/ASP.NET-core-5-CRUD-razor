using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entities
{
    [Table("playlist_track")]
    [Index(nameof(TrackId), Name = "IFK_PlaylistTrackTrackId")]
    public partial class PlaylistTrack
    {
        [Key]
        public long PlaylistId { get; set; }
        [Key]
        public long TrackId { get; set; }

        [ForeignKey(nameof(PlaylistId))]
        [InverseProperty("PlaylistTracks")]
        public virtual Playlist Playlist { get; set; }
        [ForeignKey(nameof(TrackId))]
        [InverseProperty("PlaylistTracks")]
        public virtual Track Track { get; set; }
    }
}
