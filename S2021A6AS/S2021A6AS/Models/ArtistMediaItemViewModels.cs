using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A6AS.Models
{
    public class ArtistMediaItemAddFormViewModel
    {
        public int ArtistId { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        [Display(Name = "Descriptive caption")]
        public string Caption { get; set; }

        // TODO 09 - In this "Form" class, the property type is string, and the data type is upload
        // The DataType.Upload uses the Views > Shared > EditorTemplates > Upload.cshtml template
        [Required]
        [Display(Name = "ArtistMediaItem")]
        [DataType(DataType.Upload)]
        public string ArtistMediaItemUpload { get; set; }
    }

    public class ArtistMediaItemAddViewModel
    {
        public int ArtistId { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        public string Caption { get; set; }

        // TODO 11 - In this "Form" class, the property type is HttpPostedFileBase, and the data type is upload
        [Required]
        public HttpPostedFileBase ArtistMediaItemUpload { get; set; }
    }

    // TODO 05 - View model class for ArtistMediaItem info (no ArtistMediaItem data/bytes)
    // Notice the presence of the identifying (Id, StringId) and descriptive data (Timestamp, Caption)
    public class ArtistMediaItemBaseViewModel
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

    // TODO 07 - View model class for ArtistMediaItem data/bytes
    public class ArtistMediaItemContentViewModel : ArtistMediaItemBaseViewModel
    {
        public int Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}