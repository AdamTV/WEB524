using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A1AS.Models
{
    public class EmployeeAddViewModel
    {
        const int YEARS_TO_SUBTRACT = -30;

        public EmployeeAddViewModel()
        {
            BirthDate = DateTime.Now.AddYears(YEARS_TO_SUBTRACT);
            HireDate = DateTime.Now;
        }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Text)]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(30)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
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
        [Display(Name = "Postal Code")]
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
    }
}