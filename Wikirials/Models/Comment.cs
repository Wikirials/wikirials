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
        public string Text { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        public int TutorialID { get; set; }

        
        public virtual ApplicationUser User { get; set; }
        
        public virtual Tutorial Tutorial { get; set; }
    }
}