using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wikirials.Models
{
    public class Tutorial
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string ContentType { get; set; }


        public virtual ApplicationUser User { get; set; }
        
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<FileMain> FileMains { get; set; }
    }
}