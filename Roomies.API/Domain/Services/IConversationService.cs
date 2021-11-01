using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface IConversationService
    {
        Task<IEnumerable<Conversation>> ListAsync();
        Task<ConversationResponse> GetByIdAsync(int id);
        Task<ConversationResponse> SaveAsync(Conversation conversation);
        Task<ConversationResponse> DeleteAsync(int id);
    }
}
