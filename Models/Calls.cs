using System;
using System.Collections.Generic;

namespace CallCenter.Models
{
    public partial class Calls
    {
        public Calls()
        {
            CallDetails = new HashSet<CallDetails>();
        }

        public int CallId { get; set; }
        public int CallerIndividualId { get; set; }
        public int CallAttendeeId { get; set; }
        public int CallStateId { get; set; }
        public DateTime Date { get; set; }

        public virtual Staffs CallAttendee { get; set; }
        public virtual States CallState { get; set; }
        public virtual Individuals CallerIndividual { get; set; }
        public virtual ICollection<CallDetails> CallDetails { get; set; }
    }
}
