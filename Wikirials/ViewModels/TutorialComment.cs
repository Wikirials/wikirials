using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wikirials.Models;

namespace Wikirials.ViewModels
{
    public class TutorialComment
    {
        public Tutorial Tutorials;
        public Comment Comment;
        public IEnumerable<Comment> Comments;
    }
}