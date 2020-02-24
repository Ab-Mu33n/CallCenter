using System;
using System.Collections.Generic;

namespace CallCenter.Models
{
    public partial class CallCategory
    {
        public CallCategory()
        {
            CallDetails = new HashSet<CallDetails>();
        }

        public int CallCategoryId { get; set; }
        public string CallCategory1 { get; set; }
        public string CategoryDetails { get; set; }

        public virtual ICollection<CallDetails> CallDetails { get; set; }
    }
}
