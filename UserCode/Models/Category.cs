using System;
using System.Collections.Generic;

namespace UserCode.Models
{
    public partial class Category
    {
        public Category()
        {
            Comics = new HashSet<Comic>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Comic> Comics { get; set; }
    }
}
