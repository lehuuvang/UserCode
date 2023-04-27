using System;
using System.Collections.Generic;

namespace UserCode.Models
{
    public partial class AuthorDetail
    {
        public Guid ComicId { get; set; }
        public int AuthorId { get; set; }
        public bool? Role { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual Comic Comic { get; set; } = null!;
    }
}
