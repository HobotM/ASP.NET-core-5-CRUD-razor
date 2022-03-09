using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entities
{
    [Table("invoice_items")]
    [Index(nameof(InvoiceId), Name = "IFK_InvoiceLineInvoiceId")]
    [Index(nameof(TrackId), Name = "IFK_InvoiceLineTrackId")]
    public partial class InvoiceItem
    {
        [Key]
        public long InvoiceLineId { get; set; }
        public long InvoiceId { get; set; }
        public long TrackId { get; set; }
        [Required]
        [Column(TypeName = "NUMERIC(10,2)")]
        public byte[] UnitPrice { get; set; }
        public long Quantity { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        [InverseProperty("InvoiceItems")]
        public virtual Invoice Invoice { get; set; }
        [ForeignKey(nameof(TrackId))]
        [InverseProperty("InvoiceItems")]
        public virtual Track Track { get; set; }
    }
}
