using Roomies.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Repositories
{
    public interface IUserPaymentMethodRepository
    {
        Task<IEnumerable<UserPaymentMethod>> ListAsync();
        Task<IEnumerable<UserPaymentMethod>> ListByPaymentMethodIdAsync(int paymentMethodId);
        Task<IEnumerable<UserPaymentMethod>> ListByUserIdAsync(int userId);
        void Remove(UserPaymentMethod userPaymentMethod);
        Task AddAsync(UserPaymentMethod userPaymentMethod);
        Task<UserPaymentMethod> FindByUserIdAndPaymentMethodId(int userId, int paymentMethodId);
        Task AssignUserPaymentMethodAsync(int userId, int paymentMethodId);
        Task UnassignUserPaymentMethodAsync(int userId, int paymentMethodId);

    }
}
