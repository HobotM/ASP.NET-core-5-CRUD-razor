using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entities
{
    [Table("employees")]
    [Index(nameof(ReportsTo), Name = "IFK_EmployeeReportsTo")]
    public partial class Employee
    {
        public Employee()
        {
            Customers = new HashSet<Customer>();
            InverseReportsToNavigation = new HashSet<Employee>();
        }

        [Key]
        public long EmployeeId { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string FirstName { get; set; }
        [Column(TypeName = "NVARCHAR(30)")]
        public string Title { get; set; }
        public long? ReportsTo { get; set; }
        [Column(TypeName = "DATETIME")]
        public byte[] BirthDate { get; set; }
        [Column(TypeName = "DATETIME")]
        public byte[] HireDate { get; set; }
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
        [Column(TypeName = "NVARCHAR(60)")]
        public string Email { get; set; }

        [ForeignKey(nameof(ReportsTo))]
        [InverseProperty(nameof(Employee.InverseReportsToNavigation))]
        public virtual Employee ReportsToNavigation { get; set; }
        [InverseProperty(nameof(Customer.SupportRep))]
        public virtual ICollection<Customer> Customers { get; set; }
        [InverseProperty(nameof(Employee.ReportsToNavigation))]
        public virtual ICollection<Employee> InverseReportsToNavigation { get; set; }
    }
}
