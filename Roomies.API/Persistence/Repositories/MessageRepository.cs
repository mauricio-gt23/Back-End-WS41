using Microsoft.EntityFrameworkCore;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Persistence.Contexts;
using Roomies.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Persistence.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task<Message> FindById(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<Message>> ListAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<IEnumerable<Message>> ListByConversationIdAsync(int conversationId)
        {
            return await _context.Messages
                .Where(p => p.ConversationId == conversationId)
                .Include(p => p.Conversation)
                .ToListAsync();
        }

        public void Remove(Message message)
        {
            _context.Messages.Remove(message);
        }

        public void Update(Message message)
        {
            _context.Messages.Update(message);
        }
    }
}
