using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services;
using Roomies.API.Extensions;
using Roomies.API.Resources;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Controllers
{
    [Route("/api/users/{userId}/paymentMethods")]
    public class UserPaymentMethodsController:ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IUserService _userService;
        private readonly IUserPaymentMethodService _userPaymentMethodService;
        private readonly IMapper _mapper;

        public UserPaymentMethodsController(IPaymentMethodService paymentMethodService, IUserService userService, IUserPaymentMethodService userPaymentMethodService, IMapper mapper)
        {
            _paymentMethodService = paymentMethodService;
            _userService = userService;
            _userPaymentMethodService = userPaymentMethodService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentMethodResource>> GetAllByUserTdAsync(int userId)
        {
            var paymentMethods = await _paymentMethodService.ListByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<PaymentMethodResource>>(paymentMethods);

            return resources;
        }

        [HttpPost("{paymentMethodId}")]
        public async Task<IActionResult> AssignUserPaymentMethod(int userId, int paymentMethodId)
        {
            var result = await _userPaymentMethodService.AssignUserPaymentMethodAsync(userId, paymentMethodId);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentMethodResource = _mapper.Map<PaymentMethod, PaymentMethodResource>(result.Resource.PaymentMethod);

            return Ok(paymentMethodResource);
        }
    }
}
