using S2021A2AS.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A2AS.Models
{
    public class InvoiceBaseViewModel
    {
        [Key]
        [Display(Name = "Invoice Identifier")]
        public int InvoiceId { get; set; }

        [Display(Name = "Customer Identifier")]
        public int CustomerId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        [StringLength(70)]
        [DataType(DataType.Text)]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [StringLength(40)]
        [DataType(DataType.Text)]
        [Display(Name = "Billing City")]
        public string BillingCity { get; set; }

        [StringLength(40)]
        [DataType(DataType.Text)]
        [Display(Name = "Billing State")]
        public string BillingState { get; set; }

        [StringLength(40)]
        [DataType(DataType.Text)]
        [Display(Name = "Billing Country")]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        [DataType(DataType.Text)]
        [Display(Name = "Billing Postal Code")]
        public string BillingPostalCode { get; set; }

        [Display(Name = "Invoice Total")]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public virtual Customer Customer { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}