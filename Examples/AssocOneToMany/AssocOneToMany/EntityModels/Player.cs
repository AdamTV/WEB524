using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssocOneToMany.EntityModels
{
    public class Player
    {
        public Player()
        {
            BirthDate = DateTime.Now.AddYears(-25);
        }

        public int Id { get; set; }
        public int UniformNumber { get; set; }
        public string PlayerName { get; set; }
        public string Position { get; set; }
        public string Height { get; set; }
        public int Weight { get; set; }
        public DateTime BirthDate { get; set; }
        public int YearsExperience { get; set; }
        public string College { get; set; }

        [Required]
        public Team Team { get; set; }
    }

}