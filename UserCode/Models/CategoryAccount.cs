using System;
using System.Collections.Generic;

namespace UserCode.Models
{
    public partial class CategoryAccount
    {
        public CategoryAccount()
        {
            Accounts = new HashSet<Account>();
        }

        public int CategoryAccId { get; set; }
        public string? CategoryAccName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
