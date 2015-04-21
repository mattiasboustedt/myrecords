using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyRecords.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Country { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }


        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public virtual ICollection<Album> Album { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}