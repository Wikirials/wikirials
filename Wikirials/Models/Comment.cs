using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wikirials.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        
        public virtual ApplicationUser User { get; set; }

        public virtual Tutorial Tutorial { get; set; }
    }
}