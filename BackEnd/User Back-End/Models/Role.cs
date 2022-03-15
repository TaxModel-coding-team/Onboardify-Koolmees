﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User_Back_End.Models
{
    public class Role
    {
        [Key]
        [Required]
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}