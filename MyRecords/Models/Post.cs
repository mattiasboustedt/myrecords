using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyRecords.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime DatePosted { get; set; }
        public string PostReceiverId { get; set; }
        public string PostSenderId { get; set; }


        [ForeignKey("PostReceiverId")]
        public virtual ApplicationUser PostReceiver { get; set; }
       
        [ForeignKey("PostSenderId")]
        public virtual ApplicationUser PostSender { get; set; }

    }
}