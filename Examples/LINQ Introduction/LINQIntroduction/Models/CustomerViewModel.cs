using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LINQIntroduction.Models
{
    public class CustomerAddViewModel
    {
        public CustomerAddViewModel() { }

        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [StringLength(80)]
        public string Company { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [Required]
        [StringLength(60)]
        public string Email { get; set; }
    }

    public class CustomerBaseViewModel : CustomerAddViewModel
    {
        public CustomerBaseViewModel() { }

        [Key]
        public int CustomerId { get; set; }
    }

    // TODO: 01 - This is the only new view model, a string for "search by name"
    // We use it to search for a country name, or a person name, or a company name
    // We created a view model so that it would have a nice display prompt,
    // and participate in data validation by using a data annotation
    public class SearchByNameViewModel
    {
        [Required, StringLength(80)]
        [Display(Name = "All or part of name")]
        public string Name { get; set; }
    }

}

