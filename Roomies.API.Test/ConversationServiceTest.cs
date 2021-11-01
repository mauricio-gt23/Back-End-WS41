using NUnit.Framework;
using Moq;
using FluentAssertions;
using Roomies.API.Domain.Repositories;
using Roomies.API.Services;
using Roomies.API.Domain.Services.Communications;
using System.Threading.Tasks;
using System.Collections.Generic;
using Roomies.API.Domain.Models;


namespace Roomies.API.Test
{
    public class ConversationServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoConversationReturnsEmptyCollection()
        {
            // Arrange

            var mockConversationRepository = GetDefaultIConversationRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            mockConversationRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Conversation>());

            var service = new ConversationService(mockUnitOfWork.Object, mockConversationRepository.Object);

            // Act

            List<Conversation> result = (List<Conversation>)await service.ListAsync();
            var conversationCount = result.Count;

            // Assert

            conversationCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsCategoryNotFoundResponse()
        {
            // Arrange
            var mockConversationRepository = GetDefaultIConversationRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var conversationId = 1;
            Conversation conversation = new Conversation();
            mockConversationRepository.Setup(r => r.FindById(conversationId)).Returns(Task.FromResult<Conversation>(null));
            var service = new ConversationService(mockUnitOfWork.Object, mockConversationRepository.Object);

            // Act
            ConversationResponse result = await service.GetByIdAsync(conversationId);
            var message = result.Message;

            // Assert
            message.Should().Be("Conversación inexistente");
        }

        private Mock<IConversationRepository> GetDefaultIConversationRepositoryInstance()
        {
            return new Mock<IConversationRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}