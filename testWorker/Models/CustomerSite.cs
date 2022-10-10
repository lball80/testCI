using System;
using System.Collections.Generic;

namespace testWorker.Models
{
    public partial class CustomerSite
    {
        public string Customer { get; set; } = null!;
        public string AdditionalAddress { get; set; } = null!;
        public string CustomerSiteCode { get; set; } = null!;

        public virtual Customer CustomerNavigation { get; set; } = null!;
    }
}
