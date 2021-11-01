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
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ConversationsController:ControllerBase
    {
        private readonly IConversationService _conversationService;
        private readonly IMapper _mapper;

        public ConversationsController(IConversationService conversationService, IMapper mapper)
        {
            _conversationService = conversationService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List all Conversations",
           Description = "List of Conversations",
           OperationId = "ListAllConversations"
           )]
        [SwaggerResponse(200, "List of Conversations", typeof(IEnumerable<ConversationResource>))]


        [HttpGet]
        public async Task<IEnumerable<ConversationResource>> GetAllAsync()
        {
            var conversations = await _conversationService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Conversation>, IEnumerable<ConversationResource>>(conversations);

            return resources;
        }

        //[HttpGet]
        //public async Task<IEnumerable<ConversationResource>> GetAllByUserIdAsync(int userId)
        //{
        //    var conversations = await _conversationService.ListByUserIdAsync(userId);
        //    var resources = _mapper.Map<IEnumerable<Conversation, IEnumerable<ConversationResource>>(conversations);

        //    return resources;
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ConversationResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _conversationService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var conversationResource = _mapper.Map<Conversation, ConversationResource>(result.Resource);
            return Ok(conversationResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _conversationService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var conversationResource = _mapper.Map<Conversation, ConversationResource>(result.Resource);

            return Ok(conversationResource);

        }
    }
}
