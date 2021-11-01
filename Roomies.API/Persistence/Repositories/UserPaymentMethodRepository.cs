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
    public class UserPaymentMethodRepository : BaseRepository, IUserPaymentMethodRepository
    {
        public UserPaymentMethodRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(UserPaymentMethod userPaymentMethod)
        {
            await _context.UserPaymentMethods.AddAsync(userPaymentMethod);
        }

        public async Task AssignUserPaymentMethodAsync(int userId, int paymentMethodId)
        {
            UserPaymentMethod userPaymentMethod = await FindByUserIdAndPaymentMethodId(userId, paymentMethodId);
            if (userPaymentMethod == null)
            {
                userPaymentMethod = new UserPaymentMethod { UserId = userId, PaymentMethodId = paymentMethodId };
                await AddAsync(userPaymentMethod);
            }
        }

        public async Task<UserPaymentMethod> FindByUserIdAndPaymentMethodId(int userId, int paymentMethodId)
        {
            return await _context.UserPaymentMethods.FindAsync(userId, paymentMethodId);
        }

        public async Task<IEnumerable<UserPaymentMethod>> ListAsync()
        {
            return await _context.UserPaymentMethods.ToListAsync();
        }

        public async Task<IEnumerable<UserPaymentMethod>> ListByPaymentMethodIdAsync(int paymentMethodId)
        {
            return await _context.UserPaymentMethods
               .Where(pt => pt.PaymentMethodId == paymentMethodId)
               .Include(pt => pt.PaymentMethod)
               .Include(pt => pt.User)
               .ToListAsync();
        }

        public async Task<IEnumerable<UserPaymentMethod>> ListByUserIdAsync(int userId)
        {
            return await _context.UserPaymentMethods
               .Where(pt => pt.UserId == userId)
               .Include(pt => pt.PaymentMethod)
               .Include(pt => pt.User)
               .ToListAsync();
        }

        public void Remove(UserPaymentMethod userPaymentMethod)
        {
            _context.UserPaymentMethods.Remove(userPaymentMethod);
        }

        public async Task UnassignUserPaymentMethodAsync(int userId, int paymentMethodId)
        {
            UserPaymentMethod userPaymentMethod = await FindByUserIdAndPaymentMethodId(userId, paymentMethodId);
            if (userPaymentMethod != null)
                Remove(userPaymentMethod);
        }
    }
}
