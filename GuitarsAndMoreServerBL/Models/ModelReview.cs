using System;
using System.Collections.Generic;

#nullable disable

namespace GuitarsAndMoreServerBL.Models
{
    public partial class ModelReview
    {
        public int ModelReviewId { get; set; }
        public int ModelId { get; set; }
        public int UserId { get; set; }
        public string ModelReview1 { get; set; }
        public double ModelRate { get; set; }

        public virtual Model Model { get; set; }
        public virtual User User { get; set; }
    }
}
