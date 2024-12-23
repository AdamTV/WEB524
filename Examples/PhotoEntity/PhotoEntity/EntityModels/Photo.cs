using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoEntity.EntityModels
{
    // TODO 01 - Dedicated "Photo" design model class
    // Notice its constructor, which generates a unique string identifier
    // that can be used in the photo URL
    public class Photo
    {
        public Photo()
        {
            Timestamp = DateTime.Now;

            // StringId generator
            // Code is from Mads Kristensen
            // http://madskristensen.net/post/generate-unique-strings-and-numbers-in-c

            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            StringId = string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        // For the generated identifier
        [Required, StringLength(100)]
        public string StringId { get; set; }

        // Media item
        public byte[] Content { get; set; }
        [StringLength(200)]
        public string ContentType { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        public string Caption { get; set; }

        [Required]
        public Property Property { get; set; }
    }

}