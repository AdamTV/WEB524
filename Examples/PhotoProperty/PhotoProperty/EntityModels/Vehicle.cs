using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoProperty.EntityModels
{
    public class Vehicle
    {
        public Vehicle()
        {
            ModelYear = DateTime.Now.Year;
            MSRP = 20000;
        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Model { get; set; }

        [Required, StringLength(100)]
        public string Trim { get; set; }

        public int ModelYear { get; set; }
        public int MSRP { get; set; }

        [Required, StringLength(100)]
        public string Manufacturer { get; set; }

        // TODO 1 - Design model class needs two properties for the media item
        // Here, both can be null, but the view model classes will require values
        [StringLength(200)]
        public string PhotoContentType { get; set; }
        public byte[] Photo { get; set; }
    }

}