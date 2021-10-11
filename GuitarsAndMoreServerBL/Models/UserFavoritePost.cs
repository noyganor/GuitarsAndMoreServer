using System;
using System.Collections.Generic;

#nullable disable

namespace GuitarsAndMoreServerBL.Models
{
    public partial class UserFavoritePost
    {
        public int PostId { get; set; }
        public int UserId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
