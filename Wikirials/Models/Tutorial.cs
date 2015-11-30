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
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Content Type")]
        public string ContentType { get; set; }


        public virtual ApplicationUser User { get; set; }
        
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<FileMain> FileMains { get; set; }
    }
}