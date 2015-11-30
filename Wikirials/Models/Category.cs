using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wikirials.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Description { get; set; }


        public virtual ICollection<Tutorial> Tutorials { get; set; }
    }
}