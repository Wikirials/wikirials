using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wikirials.Models
{
    public class Chat
    {
        public int ID { get; set; }
        public int ChatID { get; set; }
        public string Password { get; set; }
        
        
        public virtual ApplicationUser User { get; set; }
        
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}