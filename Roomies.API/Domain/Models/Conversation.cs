using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Models
{
    public class Conversation
    {
        public int Id { set; get; }
        public Profile User1 { set; get; }
        public int User1Id { set; get; }
        public Profile User2 { set; get; }
        public int User2Id { set; get; }
        public DateTime DateCreation { set; get; }
        public List<Message> Messages { set; get; }
    }
}
