using System;
using System.Collections.Generic;

namespace UserCode.Models
{
    public partial class WordDetail
    {
        public Guid WordToxicId { get; set; }
        public int CommnentId { get; set; }
        public string? Content { get; set; }

        public virtual Comment Commnent { get; set; } = null!;
        public virtual WordToxic WordToxic { get; set; } = null!;
    }
}
