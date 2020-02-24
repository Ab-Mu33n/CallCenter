using System;
using System.Collections.Generic;

namespace CallCenter.Models
{
    public partial class Staffs
    {
        public Staffs()
        {
            Calls = new HashSet<Calls>();
        }

        public int StaffId { get; set; }
        public int IndividualId { get; set; }
        public int DesignationId { get; set; }

        public virtual Designations Designation { get; set; }
        public virtual Individuals Individual { get; set; }
        public virtual ICollection<Calls> Calls { get; set; }
    }
}
