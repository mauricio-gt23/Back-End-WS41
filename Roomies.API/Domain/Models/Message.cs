using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Models
{
    public class Message
    {
        public int Id { set; get; }
        public string Content { set; get; } 
        public DateTime SentDate { set; get; }
        public bool Seen { set; get; }

        public Profile User { get; set; }
        public int UserId { get; set; }
        public int ConversationId { set; get; }
        public Conversation Conversation { set; get; }
    }
}
