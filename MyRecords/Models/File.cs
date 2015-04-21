using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyRecords.Models
{
    public class File
    {

        public int FileId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }

        public int? ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }



    }
}