﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCampoDijital.Model
{
    public abstract class User
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Surname {  get; set; }
        [MaxLength(250)]
        [Required]
        public string Password {  get; set; }

        protected User()
        {
            this.Name = string.Empty;
            this.Surname = string.Empty;
            this.Password = string.Empty;   
        }

    }
}
