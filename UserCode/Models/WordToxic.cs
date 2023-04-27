using System;
using System.Collections.Generic;

namespace UserCode.Models
{
    public partial class WordToxic
    {
        public WordToxic()
        {
            WordDetails = new HashSet<WordDetail>();
        }

        public Guid WordToxicId { get; set; }
        public string? WordToxicName { get; set; }

        public virtual ICollection<WordDetail> WordDetails { get; set; }
    }
}
