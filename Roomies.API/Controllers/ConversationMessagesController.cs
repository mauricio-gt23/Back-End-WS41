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
    [Route("/api/conversations/{conversationId}/messages")]
    public class ConversationMessagesController
    {
        private IMessageService _messageService;
        private readonly IMapper _mapper;

        public ConversationMessagesController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<MessageResource>> GetAllByConversationIdAsync(int conversationId)
        {
            var messages = await _messageService.ListByConversationIdAsync(conversationId);
            var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);

            return resources;
        }
    }
}
