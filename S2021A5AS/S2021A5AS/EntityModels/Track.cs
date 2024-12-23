using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace S2021A5AS.EntityModels
{
    [Table("Track")]
    public partial class Track
    {
        [Required]
        [StringLength(50)]
        public string Clerk { get; set; }
        [Required]
        [StringLength(512)]
        public string Composers { get; set; }
        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
