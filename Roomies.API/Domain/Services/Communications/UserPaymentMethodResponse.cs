using Roomies.API.Domain.Models;
using Supermarket.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services.Communications
{
    public class UserPaymentMethodResponse : BaseResponse<ProfilePaymentMethod>
    {
        public UserPaymentMethodResponse(ProfilePaymentMethod resource) : base(resource)
        {
        }

        public UserPaymentMethodResponse(string message) : base(message)
        {
        }
    }
}
