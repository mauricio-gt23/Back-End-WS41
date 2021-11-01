using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface IUserPaymentMethodService
    {
        Task<IEnumerable<UserPaymentMethod>> ListAsync();
        Task<IEnumerable<UserPaymentMethod>> ListByPaymentMethodIdAsync(int paymentMethodId);
        Task<IEnumerable<UserPaymentMethod>> ListByUserIdAsync(int userId);
        Task<UserPaymentMethodResponse> AssignUserPaymentMethodAsync(int userId, int paymentMethodId);
        Task<UserPaymentMethodResponse> UnassignUserPaymentMethodAsync(int userId, int paymentMethodId);
    }
}
