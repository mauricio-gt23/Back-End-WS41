using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Repositories
{
    public interface IConversationRepository
    {
        Task<IEnumerable<Conversation>> ListAsync();
        Task<Conversation> FindById(int id);
        Task AddAsync(Conversation conversation);
        //Task<IEnumerable<Conversation>> ListByUserIdAsync(int userId);
        void Update(Conversation conversation);
        void Remove(Conversation conversation);

    }
}
