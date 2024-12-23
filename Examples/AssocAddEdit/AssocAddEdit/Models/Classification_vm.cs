using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssocAddEdit.Models
{
    // TODO: 0X - Country view model class
    public class ClassificationBaseViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Classification Name")]
        public string Name { get; set; }
    }
}