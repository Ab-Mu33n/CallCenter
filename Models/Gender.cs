using System;
using System.Collections.Generic;

namespace CallCenter.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Individuals = new HashSet<Individuals>();
        }

        public int GenderId { get; set; }
        public string GenderAbbreviation { get; set; }
        public string Gender1 { get; set; }

        public virtual ICollection<Individuals> Individuals { get; set; }
    }
}
