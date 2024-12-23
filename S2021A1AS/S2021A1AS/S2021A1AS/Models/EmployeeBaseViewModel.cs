using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A1AS.Models
{
    public class EmployeeBaseViewModel : EmployeeAddViewModel
    {
        // [Key] = Primary Key
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }
    }
}   