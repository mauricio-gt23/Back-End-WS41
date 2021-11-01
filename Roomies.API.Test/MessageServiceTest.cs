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
    public class MessageServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoMessageReturnsEmptyCollection()
        {
            // Arrange

            var mockMessageRepository = GetDefaultIMessageRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            mockMessageRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Message>());

            var service = new MessageService(mockMessageRepository.Object, mockUnitOfWork.Object);

            // Act

            List<Message> result = (List<Message>)await service.ListAsync();
            var messageCount = result.Count;

            // Assert

            messageCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsMessageNotFoundResponse()
        {
            // Arrange
            var mockMessageRepository = GetDefaultIMessageRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var messageId = 1;
            Message message = new Message();
            mockMessageRepository.Setup(r => r.FindById(messageId)).Returns(Task.FromResult<Message>(null));
            var service = new MessageService(mockMessageRepository.Object, mockUnitOfWork.Object);

            // Act
            MessageResponse result = await service.GetByIdAsync(messageId);
            var messageResult = result.Message;

            // Assert
            messageResult.Should().Be("Mensaje inexistente");
        }

        private Mock<IMessageRepository> GetDefaultIMessageRepositoryInstance()
        {
            return new Mock<IMessageRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}