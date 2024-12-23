using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2021A6AS.Models
{
    public class AlbumAddViewModel
    {
        public string Coordinator { get; set; }
        public string Genre { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string UrlAlbum { get; set; }
        public string Summary { get; set; }
    }
}