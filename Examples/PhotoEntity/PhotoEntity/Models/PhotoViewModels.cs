using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoEntity.Models
{
    public class PhotoAddFormViewModel
    {
        public int PropertyId { get; set; }

        [Display(Name = "Property information")]
        public string PropertyInfo { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        [Display(Name = "Descriptive caption")]
        public string Caption { get; set; }

        // TODO 09 - In this "Form" class, the property type is string, and the data type is upload
        // The DataType.Upload uses the Views > Shared > EditorTemplates > Upload.cshtml template
        [Required]
        [Display(Name = "Photo")]
        [DataType(DataType.Upload)]
        public string PhotoUpload { get; set; }
    }

    public class PhotoAddViewModel
    {
        public int PropertyId { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        public string Caption { get; set; }

        // TODO 11 - In this "Form" class, the property type is HttpPostedFileBase, and the data type is upload
        [Required]
        public HttpPostedFileBase PhotoUpload { get; set; }
    }

    // TODO 05 - View model class for photo info (no photo data/bytes)
    // Notice the presence of the identifying (Id, StringId) and descriptive data (Timestamp, Caption)
    public class PhotoBaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Added on date/time")]
        public DateTime Timestamp { get; set; }

        // For the generated identifier
        [Required, StringLength(100)]
        [Display(Name = "Unique identifier")]
        public string StringId { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        [Display(Name = "Descriptive caption")]
        public string Caption { get; set; }
    }

    // TODO 07 - View model class for photo data/bytes
    public class PhotoContentViewModel
    {
        public int Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }

}
