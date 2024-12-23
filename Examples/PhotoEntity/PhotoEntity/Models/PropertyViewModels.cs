using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoEntity.Models
{
    public class PropertyAddFormViewModel
    {
        public PropertyAddFormViewModel()
        {
            Price = 300000;
        }

        [Required, StringLength(200)]
        [Display(Name = "Street address")]
        public string Address { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "City / town")]
        public string City { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Asking price")]
        public int Price { get; set; }
    }

    public class PropertyAddViewModel
    {
        [Required, StringLength(200)]
        [Display(Name = "Street address")]
        public string Address { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "City / town")]
        public string City { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Asking price")]
        public int Price { get; set; }
    }
    
    public class PropertyBaseViewModel : PropertyAddViewModel
    {
        public int Id { get; set; }
    }

    // TODO 04 - View model class for an object with a photo info collection
    public class PropertyWithPhotoStringIdsViewModel : PropertyBaseViewModel
    {
        public PropertyWithPhotoStringIdsViewModel()
        {
            Photos = new List<PhotoBaseViewModel>();
        }

        public IEnumerable<PhotoBaseViewModel> Photos { get; set; }
    }

}
