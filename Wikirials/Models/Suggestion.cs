using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Wikirials.Models
{
    public class Suggestion
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}