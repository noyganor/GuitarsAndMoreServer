using System;
using System.Collections.Generic;

#nullable disable

namespace GuitarsAndMoreServerBL.Models
{
    public partial class Town
    {
        public Town()
        {
            Posts = new HashSet<Post>();
        }

        public int TownId { get; set; }
        public string Town1 { get; set; }
        public int? AreaId { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
