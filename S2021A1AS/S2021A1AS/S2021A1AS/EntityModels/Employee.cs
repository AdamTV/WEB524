using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace S2021A1AS.EntityModels
{
    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Customers = new HashSet<Customer>();
            Employee1 = new HashSet<Employee>();
        }

        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [StringLength(30)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? ReportsTo { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }

        [StringLength(70)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [StringLength(40)]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [StringLength(40)]
        [DataType(DataType.Text)]
        public string State { get; set; }

        [StringLength(40)]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [StringLength(10)]
        [DataType(DataType.Text)]
        public string PostalCode { get; set; }

        [StringLength(24)]
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        [StringLength(24)]
        [DataType(DataType.Text)]
        public string Fax { get; set; }

        [StringLength(60)]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee1 { get; set; }

        public virtual Employee Employee2 { get; set; }
    }
}
