using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entities
{
    [Table("media_types")]
    public partial class MediaType
    {
        public MediaType()
        {
            Tracks = new HashSet<Track>();
        }

        [Key]
        public long MediaTypeId { get; set; }
        [Column(TypeName = "NVARCHAR(120)")]
        public string Name { get; set; }

        [InverseProperty(nameof(Track.MediaType))]
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
