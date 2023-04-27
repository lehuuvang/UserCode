using System;
using System.Collections.Generic;

namespace UserCode.Models
{
    public partial class Posting
    {
        public int PostId { get; set; }
        public string? PostContent { get; set; }
        public bool? PostStatus { get; set; }
        public DateTime? Date { get; set; }
        public int AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
