using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entities
{
    [Table("invoices")]
    [Index(nameof(CustomerId), Name = "IFK_InvoiceCustomerId")]
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        [Key]
        public long InvoiceId { get; set; }
        public long CustomerId { get; set; }
        [Required]
        [Column(TypeName = "DATETIME")]
        public byte[] InvoiceDate { get; set; }
        [Column(TypeName = "NVARCHAR(70)")]
        public string BillingAddress { get; set; }
        [Column(TypeName = "NVARCHAR(40)")]
        public string BillingCity { get; set; }
        [Column(TypeName = "NVARCHAR(40)")]
        public string BillingState { get; set; }
        [Column(TypeName = "NVARCHAR(40)")]
        public string BillingCountry { get; set; }
        [Column(TypeName = "NVARCHAR(10)")]
        public string BillingPostalCode { get; set; }
        [Required]
        [Column(TypeName = "NUMERIC(10,2)")]
        public byte[] Total { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Invoices")]
        public virtual Customer Customer { get; set; }
        [InverseProperty(nameof(InvoiceItem.Invoice))]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
