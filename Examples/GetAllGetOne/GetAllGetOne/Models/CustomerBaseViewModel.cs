using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetAllGetOne.Models
{
    public class CustomerBaseViewModel : CustomerAddViewModel
    {
        [Key]
        public int CustomerId { get; set; }
    }
}