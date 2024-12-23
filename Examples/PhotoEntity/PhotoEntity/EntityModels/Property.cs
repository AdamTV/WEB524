using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoEntity.EntityModels
{
    public class Property
    {
        public Property()
        {
            Photos = new List<Photo>();
            Price = 300000;
        }

        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Address { get; set; }

        [Required, StringLength(100)]
        public string City { get; set; }

        public int Price { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }

}