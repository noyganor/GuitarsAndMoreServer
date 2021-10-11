using System;
using System.Collections.Generic;

#nullable disable

namespace GuitarsAndMoreServerBL.Models
{
    public partial class UserReview
    {
        public UserReview()
        {
            Posts = new HashSet<Post>();
        }

        public int UserReviewId { get; set; }
        public int UserId { get; set; }
        public int SellerId { get; set; }
        public string UserReview1 { get; set; }
        public double UserRate { get; set; }

        public virtual User Seller { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
