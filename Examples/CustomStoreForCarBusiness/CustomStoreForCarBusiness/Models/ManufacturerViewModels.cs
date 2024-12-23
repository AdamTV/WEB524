using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomStoreForCarBusiness.Models
{
    // TODO 12 - View model classes, Manufacturer entity

    public class ManufacturerBaseViewModel
    {
        public ManufacturerBaseViewModel()
        {
            YearStarted = 1950;
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Manufacturer Name")]
        public string Name { get; set; }

        [Range(1850, Int16.MaxValue)]
        [Display(Name = "Year Started")]
        public int YearStarted { get; set; }
    }

    public class ManufacturerWithDetailViewModel : ManufacturerBaseViewModel
    {
        public ManufacturerWithDetailViewModel()
        {
            Vehicles = new List<VehicleBaseViewModel>();
        }

        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public ICollection<VehicleBaseViewModel> Vehicles { get; set; }
    }
}
