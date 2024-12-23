using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2021A5AS.Models
{
    public class ArtistAddViewModel
    {
        public string BirthName { get; set; }

        public DateTime BirthOrStartDate { get; set; }

        public string Executive { get; set; }

        public string Genre { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string UrlArtist { get; set; }
    }
}