﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wikirials.Models
{
    public class Group
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        //public string Password { get; set; }


        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Tutorial> Tutorials { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }
    }
}