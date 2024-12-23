using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A1AS.Models
{
    public class EmployeeEditViewModel
    {
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }

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

        [Required, StringLength(100)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&'])[^ ]{8,}", ErrorMessage = "Password must be 8+ characters, have 1+ digits, 1+ upper-case characters, 1+ lower-case characters, and 1+ special characters ( ! @ # $ % ^ &)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&'])[^ ]{8,}", ErrorMessage = "Password must be 8+ characters, have 1+ digits, 1+ upper-case characters, 1+ lower-case characters, and 1+ special characters ( ! @ # $ % ^ &)")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordCompare { get; set; }

        [Display(Name = "Employee Number")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Employee Number should consist of exactly 9 digits")]
        public string EmployeeNumber { get; set; }

        [Display(Name = "Office Level")]
        [Range(1, 10, ErrorMessage = "Office Level must be a number between 1 and 10")]
        public int OfficeLevel { get; set; }
    }
}