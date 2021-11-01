using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Resources
{
    public class ConversationResource
    {
        public int Id { set; get; }
        public ProfileResource User1 { set; get; }
        public ProfileResource User2 { set; get; }

        public DateTime DateCreation { set; get; }
    }
}
