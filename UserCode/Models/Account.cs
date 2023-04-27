using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserCode.Models
{
    public partial class Account
    {
        public Account()
        {
            Comics = new HashSet<Comic>();
            Comments = new HashSet<Comment>();
            Postings = new HashSet<Posting>();
        }

        public int AccountId { get; set; }
        public string? AccountName { get; set; }
        public string? Email { get; set; }
        public string? PassWord { get; set; }   
        public int? Phone { get; set; }
        public string? AccountImage { get; set; }
        public int? CategoryAccId { get; set; }
        public virtual CategoryAccount? CategoryAcc { get; set; } = null!;
        public virtual ICollection<Comic> Comics { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Posting> Postings { get; set; }
    }
}
