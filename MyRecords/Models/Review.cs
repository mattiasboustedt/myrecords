using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyRecords.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewContent { get; set; }
        public string ReviewerEmail { get; set; }
        public int AlbumId { get; set; }

        public virtual Album Album { get; set; }
    }
}
