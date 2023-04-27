using System;
using System.Collections.Generic;

namespace UserCode.Models
{
    public partial class Image
    {
        public Guid ImageId { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? ChapterId { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public string? ImageUrl4 { get; set; }
        public string? ImageUrl5 { get; set; }
        public string? ImageUrl6 { get; set; }
        public string? ImageUrl7 { get; set; }
        public string? ImageUrl8 { get; set; }
        public string? ImageUrl9 { get; set; }
        public string? ImageUrl10 { get; set; }
        public string? ImageUrl11 { get; set; }
        public string? ImageUrl12 { get; set; }
        public string? ImageUrl13 { get; set; }
        public string? ImageUrl14 { get; set; }

        public virtual Chapter? Chapter { get; set; }
    }
}
