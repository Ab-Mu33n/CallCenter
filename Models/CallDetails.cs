using System;
using System.Collections.Generic;

namespace CallCenter.Models
{
    public partial class CallDetails
    {
        public int CallDescriptionId { get; set; }
        public string CallSummary { get; set; }
        public int CallCategoryId { get; set; }
        public int CallDuration { get; set; }
        public int CallId { get; set; }

        public virtual Calls Call { get; set; }
        public virtual CallCategory CallCategory { get; set; }
    }
}
