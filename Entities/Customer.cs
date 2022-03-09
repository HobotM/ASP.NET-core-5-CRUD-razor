using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entities
{
    [Table("customers")]
    [Index(nameof(SupportRepId), Name = "IFK_CustomerSupportRepId")]
    public partial class Customer
    {
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        [Key]
        public long CustomerId { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(40)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string LastName { get; set; }
        [Column(TypeName = "NVARCHAR(80)")]
        public string Company { get; set; }
        [Column(TypeName = "NVARCHAR(70)")]
        public string Address { get; set; }
        [Column(TypeName = "NVARCHAR(40)")]
        public string City { get; set; }
        [Column(TypeName = "NVARCHAR(40)")]
        public string State { get; set; }
        [Column(TypeName = "NVARCHAR(40)")]
        public string Country { get; set; }
        [Column(TypeName = "NVARCHAR(10)")]
        public string PostalCode { get; set; }
        [Column(TypeName = "NVARCHAR(24)")]
        public string Phone { get; set; }
        [Column(TypeName = "NVARCHAR(24)")]
        public string Fax { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(60)")]
        public string Email { get; set; }
        public long? SupportRepId { get; set; }

        [ForeignKey(nameof(SupportRepId))]
        [InverseProperty(nameof(Employee.Customers))]
        public virtual Employee SupportRep { get; set; }
        [InverseProperty(nameof(Invoice.Customer))]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
