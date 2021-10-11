using System;
using System.Collections.Generic;

#nullable disable

namespace GuitarsAndMoreServerBL.Models
{
    public partial class Area
    {
        public Area()
        {
            Towns = new HashSet<Town>();
        }

        public int AreaId { get; set; }
        public string Area1 { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
