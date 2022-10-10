using System;
using System.Collections.Generic;

namespace testWorker.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerSites = new HashSet<CustomerSite>();
        }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int LastYearExpense { get; set; }

        public virtual ICollection<CustomerSite> CustomerSites { get; set; }
    }
}
