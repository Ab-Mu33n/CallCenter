using System;
using System.Collections.Generic;

namespace CallCenter.Models
{
    public partial class States
    {
        public States()
        {
            Calls = new HashSet<Calls>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public string Details { get; set; }

        public virtual ICollection<Calls> Calls { get; set; }
    }
}
