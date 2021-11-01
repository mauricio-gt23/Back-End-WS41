using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services;
using Roomies.API.Resources;

namespace Roomies.API.Controllers
{
    [Route("/api/plans/{planId}/users")]
    public class PlanUsersController
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public PlanUsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserResource>> GetAllByPlanIdAsync(int planId)
        {
            var users = await _userService.ListByPlanIdAsync(planId);
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);

            return resources;
        }
    }
}
