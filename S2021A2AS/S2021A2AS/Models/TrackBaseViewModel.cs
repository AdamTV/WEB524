using S2021A2AS.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S2021A2AS.Models
{
    public class TrackBaseViewModel
    {
        [Key]
        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.Text)]
        [Display(Name="Track Name")]
        public string Name { get; set; }

        [Display(Name = "Album Identifier")]
        public int? AlbumId { get; set; }

        [Display(Name = "Media Type Identifier")]
        public int MediaTypeId { get; set; }

        [Display(Name = "Genre Identifier")]
        public int? GenreId { get; set; }

        [StringLength(220)]
        [DataType(DataType.Text)]
        [Display(Name = "Composer Name(s)")]
        public string Composer { get; set; }

        [Display(Name = "Track Length (Milliseconds)")]
        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        public virtual Album Album { get; set; }

        public virtual Genre Genre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }

        public virtual MediaType MediaType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}