using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muse.Models
{
    public class ThoughtAuthor
    {
        public string name { get; set; }
    }
    public class Thought
    {
        public string quote { get; set; }
        public ThoughtAuthor thoughtAuthor { get; set; }
    }
    public class ThoughtStream
    {
        public Thought[] thoughts { get; set; }
    }

    public class ForbesThoughts
    {
        public ThoughtStream thoughtStream { get; set; }
    }
}
