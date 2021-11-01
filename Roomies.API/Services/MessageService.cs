using Roomies.API.Domain.Models;
using Roomies.API.Domain.Persistence.Repositories;
using Roomies.API.Domain.Repositories;
using Roomies.API.Domain.Services;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IProfileRepository _userRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IMessageRepository messageRepository, IUnitOfWork unitOfWork, IProfileRepository userRepository = null, IConversationRepository conversationRepository = null)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _conversationRepository = conversationRepository;
        }

        public async Task<MessageResponse> DeleteAsync(int id)
        {
            var existingMessage = await _messageRepository.FindById(id);

            if (existingMessage == null)
                return new MessageResponse("Mensaje inexistente");

            try
            {
                _messageRepository.Remove(existingMessage);
                await _unitOfWork.CompleteAsync();

                return new MessageResponse(existingMessage);
            }
            catch (Exception ex)
            {
                return new MessageResponse($"Un error ocurrió al buscar el mensaje: {ex.Message}");
            }
        }

        public async Task<MessageResponse> GetByIdAsync(int id)
        {
            var existingMessage = await _messageRepository.FindById(id);

            if (existingMessage == null)
                return new MessageResponse("Mensaje inexistente");

            return new MessageResponse(existingMessage);
        }

        public async Task<IEnumerable<Message>> ListAsync()
        {
            return await _messageRepository.ListAsync();
        }

        public async Task<IEnumerable<Message>> ListByConversationIdAsync(int conversationId)
        {
            return await _messageRepository.ListByConversationIdAsync(conversationId);
        }

        public async Task<MessageResponse> SaveAsync(Message message,int conversationId, int userId)
        {

            var existingUser = await _userRepository.FindById(userId);

            if (existingUser == null)
                return new MessageResponse("Usuario inexistente");

            var existingConversation = await _conversationRepository.FindById(conversationId);

            if (existingConversation == null)
                return new MessageResponse("Conversación inexistente");

            try
            {

                message.UserId = userId;
                message.ConversationId = conversationId;

                await _messageRepository.AddAsync(message);
                await _unitOfWork.CompleteAsync();

                return new MessageResponse(message);
            }
            catch (Exception ex)
            {
                return new MessageResponse($"Un error ocurrió al guardar el mensaje: {ex.Message}");
            }
        }
    }
}
