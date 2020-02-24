using System;
using System.Collections.Generic;

namespace CallCenter.Models
{
    public partial class Individuals
    {
        public Individuals()
        {
            Calls = new HashSet<Calls>();
            Staffs = new HashSet<Staffs>();
        }

        public int IndividualId { get; set; }
        public string Nicnumber { get; set; }
        public string PassportNumber { get; set; }
        public string FullName { get; set; }
        public DateTime DateofBirth { get; set; }
        public int GenderId { get; set; }
        public string Address { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual ICollection<Calls> Calls { get; set; }
        public virtual ICollection<Staffs> Staffs { get; set; }
    }
}
