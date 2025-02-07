﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace myshop.Entities.Models
{
    public class ApplicationUser:IdentityUser
    {

        [Required]
        public string Name { get; set; }    

        public string Address { get; set; }

        public string City { get; set; }    
    }
}
