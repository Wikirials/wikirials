using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Wikirials.Models
{
    public class Tutorial
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public Boolean Classification { get; set; }
        public string Type { get; set; }
        public string ContentType { get; set; }

        

        public virtual ApplicationUser User { get; set; }
        
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}