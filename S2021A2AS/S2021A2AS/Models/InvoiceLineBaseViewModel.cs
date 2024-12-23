using S2021A2AS.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A2AS.Models
{
    public class InvoiceLineBaseViewModel
    {
        [Key]
        [Display(Name = "Invoice Line ID")]
        public int InvoiceLineId { get; set; }

        public int InvoiceId { get; set; }

        [Display(Name = "Track ID")]
        public int TrackId { get; set; }

        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }
        
        public int Quantity { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Track Track { get; set; }
    }
}