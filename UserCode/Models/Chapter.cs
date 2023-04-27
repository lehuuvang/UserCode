using System;
using System.Collections.Generic;

namespace UserCode.Models
{
    public partial class Chapter
    {
        public Chapter()
        {
            Comments = new HashSet<Comment>();
            Images = new HashSet<Image>();
        }

        public Guid ChapterId { get; set; }
        public string? ChapterName { get; set; }
        public int? View { get; set; }
        public DateTime? UpdateDay { get; set; }
        public Guid? ComicId { get; set; }

        public virtual Comic? Comic { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
