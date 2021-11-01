using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Resources
{
    public class MessageResource
    {
        public int Id { set; get; }
        public string Content { set; get; }
        public DateTime SentDate { set; get; }
        public bool Seen { set; get; }
        public ConversationResource Conversation { set; get; }
        public ProfileResource User { set; get; }
    }
}
