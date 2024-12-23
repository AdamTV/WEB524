using S2021A3AS.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A3AS.Models
{
    public class ArtistBaseViewModel
    {
        [Key]
        public int ArtistId { get; set; }

        [StringLength(120)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}