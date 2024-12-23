using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace AssocOneToMany.Models
{
    // Attention 06 - Team view model classes, Base, and WithPlayers

    public class TeamBaseViewModel
    {
        public TeamBaseViewModel() { }

        public int Id { get; set; }

        [Display(Name = "Team Code")]
        public string CodeName { get; set; }

        [Display(Name = "Team Name")]
        public string Name { get; set; }

        public string City { get; set; }

        [Display(Name = "Year Founded")]
        public int YearFounded { get; set; }

        public string Conference { get; set; }
        public string Division { get; set; }
        public string Stadium { get; set; }
    }

    // Attention 07 - Notice the collection of PlayerBase objects

    public class TeamWithPlayersViewModel : TeamBaseViewModel
    {
        public TeamWithPlayersViewModel()
        {
            Players = new List<PlayerBaseViewModel>();
        }

        public IEnumerable<PlayerBaseViewModel> Players { get; set; }
    }

}