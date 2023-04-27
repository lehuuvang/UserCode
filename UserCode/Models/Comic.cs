using System;
using System.Collections.Generic;

namespace UserCode.Models
{
    public partial class Comic
    {
        public Comic()
        {
            AuthorDetails = new HashSet<AuthorDetail>();
            Chapters = new HashSet<Chapter>();
            Categories = new HashSet<Category>();
        }

        public Guid ComicId { get; set; }
        public string? ComicName { get; set; }
        public string Describe { get; set; } = null!;
        public string? ComicBanner { get; set; }
        public DateTime? DateSummitted { get; set; }
        public int? Rating { get; set; }
        public string? ComicStatus { get; set; }
        public int AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<AuthorDetail> AuthorDetails { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
