using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace AssocOneToMany.Models
{
    // Attention 11 - Player view model classes, Base, and WithTeamInfo (object), and WithTeamName (flattening)

    public class PlayerBaseViewModel
    {
        public PlayerBaseViewModel()
        {
            BirthDate = DateTime.Now.AddYears(-25);
        }

        public int Id { get; set; }

        [Display(Name = "Uniform Number")]
        public int UniformNumber { get; set; }

        [Display(Name = "Player Name")]
        public string PlayerName { get; set; }

        public string Position { get; set; }
        public string Height { get; set; }
        public int Weight { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Years of Experience")]
        public int YearsExperience { get; set; }

        public string College { get; set; }
    }

    // Attention 12 - Notice the TeamBaseViewModel object

    public class PlayerWithTeamInfoViewModel : PlayerBaseViewModel
    {
        public TeamBaseViewModel Team { get; set; }
    }

    // Attention 13 - Notice the composed name, to fetch the "CodeName" property of the "Team" object

    public class PlayerWithTeamNameViewModel : PlayerBaseViewModel
    {
        [Display(Name = "Team Code")]
        public string TeamCodeName { get; set; }

        [Display(Name = "Team Name")]
        public string TeamName { get; set; }
    }
}