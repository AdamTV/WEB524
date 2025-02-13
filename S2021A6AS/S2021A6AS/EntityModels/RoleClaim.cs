﻿using System.ComponentModel.DataAnnotations;

namespace S2021A6AS.EntityModels
{
    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}