using System;
using System.Collections.Generic;

namespace UserCode.Models
{
    public partial class Author
    {
        public Author()
        {
            AuthorDetails = new HashSet<AuthorDetail>();
        }

        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<AuthorDetail> AuthorDetails { get; set; }
    }
}
