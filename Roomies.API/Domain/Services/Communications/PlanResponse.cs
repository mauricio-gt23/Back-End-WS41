﻿using Roomies.API.Domain.Models;
using Supermarket.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services.Communications
{
    public class PlanResponse : BaseResponse<Plan>
    {
        public PlanResponse(Plan resource) : base(resource)
        {
        }

        public PlanResponse(string message) : base(message)
        {
        }
    }
}
