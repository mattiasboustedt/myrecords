using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MyRecords.Models
{

    public enum Genre
    {
        Blues, Classic, Dance, House, Metal, Pop, Punk, Rock, Techno

    }
    public class Album
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public int ReleaseYear { get; set; }
        public int AmountOfTracks { get; set; }
        public int ArtistId { get; set; }
        public int PricePaid { get; set; }
        public string UserId { get; set; }
        public Genre? Genre { get; set; }


        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual ICollection<Review> Review { get; set; }

    }
}
