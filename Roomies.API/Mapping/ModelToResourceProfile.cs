﻿using AutoMapper;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using Roomies.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Mapping
{
    public class ModelToResourceProfile : AutoMapper.Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Conversation, ConversationResource>();
            CreateMap<Message, MessageResource>();
            CreateMap<PaymentMethod, PaymentMethodResource>();
            CreateMap<Plan, PlanResource>();
            CreateMap<Review, ReviewResource>();
            CreateMap<Post, PostResource>();
            CreateMap<Domain.Models.Profile, ProfileResource>();
            CreateMap<Landlord, LandlordResource>();
            CreateMap<Leaseholder, LeaseholderResource>();
            CreateMap<User, AuthenticationResponse>();



        }
    }
}
