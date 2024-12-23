using S2021A5AS.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2021A5AS.Models
{
    public class TrackAddViewModel
    {
        public string Clerk { get; set; }

        public string Composers { get; set; }

        public string Genre { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int AlbumId { get; set; }
    }
}