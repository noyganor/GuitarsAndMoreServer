using System;
using System.Collections.Generic;

#nullable disable

namespace GuitarsAndMoreServerBL.Models
{
    public partial class Model
    {
        public Model()
        {
            ModelReviews = new HashSet<ModelReview>();
            Posts = new HashSet<Post>();
        }

        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int ProducerId { get; set; }

        public virtual ICollection<ModelReview> ModelReviews { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
