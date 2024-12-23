using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace S2021A5AS.EntityModels
{
    [Table("Artist")]
    public partial class Artist
    {
        public Artist()
        {
            BirthOrStartDate = DateTime.Now;
        }

        [StringLength(50)]
        public string BirthName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthOrStartDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Executive { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(512)]
        public string UrlArtist { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
