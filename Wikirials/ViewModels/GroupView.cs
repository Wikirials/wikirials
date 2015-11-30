using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wikirials.Models;

namespace Wikirials.ViewModels
{
    public class GroupView
    {
        public Group Group;
        public Suggestion Suggestion;
        public IEnumerable<Suggestion> Suggestions;
        //public Tutorial Tutorial;
        //public IEnumerable<Tutorial> Tutorials;
    }
}