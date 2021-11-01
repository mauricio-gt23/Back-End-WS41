using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> ListAsync();
        Task<IEnumerable<Message>> ListByConversationIdAsync(int conversationId);
        Task<MessageResponse> GetByIdAsync(int id);
        Task<MessageResponse> SaveAsync(Message message,int conversationId,int userId);
        Task<MessageResponse> DeleteAsync(int id);
    }
}
