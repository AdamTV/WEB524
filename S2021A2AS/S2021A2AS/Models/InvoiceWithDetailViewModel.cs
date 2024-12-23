using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A2AS.Models
{
    public class InvoiceWithDetailViewModel : InvoiceBaseViewModel
    {
        [DataType(DataType.Text)]
        public string CustomerFirstName { get; set; }
        [DataType(DataType.Text)]
        public string CustomerLastName { get; set; }
        [DataType(DataType.Text)]
        public string CustomerCity { get; set; }
        [DataType(DataType.Text)]
        public string CustomerState { get; set; }
        [DataType(DataType.Text)]
        public string CustomerEmployeeFirstName { get; set; }
        [DataType(DataType.Text)]
        public string CustomerEmployeeLastName { get; set; }
        
        public IEnumerable<InvoiceLineWithDetailViewModel> InvoiceLines { get; set; }
    }
}