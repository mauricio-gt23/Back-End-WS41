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
    public class ConversationRepository : BaseRepository, IConversationRepository
    {
        public ConversationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Conversation conversation)
        {
            await _context.Conversations.AddAsync(conversation);
        }

        public async Task<Conversation> FindById(int id)
        {
            return await _context.Conversations.FindAsync(id);
        }

        public async Task<IEnumerable<Conversation>> ListAsync()
        {
            return await _context.Conversations.ToListAsync();
        }

        //public async Task<IEnumerable<Conversation>> ListByUserIdAsync(int userId)
        //{
        //    return await _context.Conversations
        //        .Where(p => p.User1Id == userId)
        //        .Include(p => p.User1)
        //        .ToListAsync();

        //}

        public void Remove(Conversation conversation)
        {
            _context.Conversations.Remove(conversation);
        }

        public void Update(Conversation conversation)
        {
            _context.Conversations.Update(conversation);
        }
    }
}
