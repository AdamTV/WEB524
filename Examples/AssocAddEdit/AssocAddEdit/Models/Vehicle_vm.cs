using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssocAddEdit.Models
{
    // TODO: 23 - Form-building data; can conveniently inherit from "add"

    public class VehicleAddFormViewModel : VehicleAddViewModel
    {
        // SelectList for Classification
        [Display(Name = "Classification List")]
        public SelectList ClassificationList { get; set; }

        // TODO: 24 - SelectList for the associated item
        [Display(Name = "Manufacturer List")]
        public SelectList ManufacturerList { get; set; }

        // TODO: 25 - Display the name of the associated item
        public string ManufacturerName { get; set; }
    }


    // TODO: 21 - Vehicle view model classes
    // Study the All_vm class diagram and its "readme" text, for more info/explanations

    public class VehicleAddViewModel
    {
        public VehicleAddViewModel()
        {
            ModelYear = DateTime.Now.Year;
            MSRP = 20000;
        }

        [Required, StringLength(100)]
        public string Model { get; set; }

        [Required, StringLength(100)]
        public string Trim { get; set; }

        [Range(1850, Int16.MaxValue)]
        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Sell Price")]
        public int MSRP { get; set; }

        [Required, StringLength(200)]
        public string Classification { get; set; }

        // TODO: 22 - Identifier for the associated item - required!
        [Range(1, Int32.MaxValue)]
        public int ManufacturerId { get; set; }
    }


    public class VehicleBaseViewModel : VehicleAddViewModel
    {
        public int Id { get; set; }
    }

    // TODO: 26 - More composed properties, notice the Manufacturer > Country > Name path

    public class VehicleWithDetailViewModel : VehicleBaseViewModel
    {
        [Display(Name = "Manufacturer Name")]
        public string ManufacturerName { get; set; }

        [Display(Name = "Country")]
        public string ManufacturerCountryName { get; set; }
    }

    // TODO: 27 - Form-building data, no inheritance, all custom

    public class VehicleEditFormViewModel
    {
        public int Id { get; set; }

        // To be displayed only
        public string Model { get; set; }

        // To be displayed only
        public string Trim { get; set; }

        // To be displayed only
        public int ModelYear { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Sell Price")]
        public int MSRP { get; set; }

        // To be displayed only
        public string ManufacturerName { get; set; }

        // We usually NEVER edit/change the to-one association
        // in an "edit existing" use case
    }

    public class VehicleEditViewModel
    {
        public int Id { get; set; }

        // We allow ONLY the MSRP to be edited/changed
        [Range(1, Int32.MaxValue)]
        [Display(Name = "Sell Price")]
        public int MSRP { get; set; }
    }

}
