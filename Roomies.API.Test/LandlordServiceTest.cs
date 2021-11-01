/*
 * TODO: IN PROCESS
 * 
 * using NUnit.Framework;
using Moq;
using FluentAssertions;
using Roomies.API.Domain.Repositories;
using Roomies.API.Services;
using Roomies.API.Domain.Services.Communications;
using System.Threading.Tasks;
using System.Collections.Generic;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Persistence.Repositories;
using System;

namespace Roomies.API.Test
{
    public class LandlordServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SaveLandlordWhenParametersAreTheSameReturnsCantSave()
        {
            // Arrange

            var mockLandlordRepository = GetDefaultILandlordRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            string email = "email";

            Plan plan = new Plan
            {
                Id = 1

            };

            Landlord landlord1 = new Landlord
            {
                IdUser = 1,
                Email = email

            };

            Landlord landlord2 = new Landlord
            {
                IdUser = 2,
                Email = email

            };

            List<User> users = new List<User>();
            //:(
            users.Add(landlord1);

            mockPlanRepository.Setup(u => u.AddAsync(plan)).Returns(Task.FromResult<Plan>(plan));
            mockPlanRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Plan>(plan));

            mockUserRepository.Setup(u => u.ListAsync()).Returns(Task.FromResult<IEnumerable<User>>(users as IEnumerable<User>));

            mockLandlordRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Landlord>(landlord1));
            mockLandlordRepository.Setup(u => u.AddAsync(landlord2)).Returns(Task.FromResult<Landlord>(null));

            var service = new LandlordService(mockLandlordRepository.Object, mockUnitOfWork.Object, mockPlanRepository.Object, mockPostRepository.Object, mockUserRepository.Object);



            // Act


            LandlordResponse result = await service.SaveAsync(landlord2, 1);
            var message = result.Message;


            // Assert

            message.Should().Be("El email ingresado ya existe");
        }


        [Test]
        public async Task SaveLandlordWhenParametersAreDifferentCanSave()
        {
            // Arrange

            var mockLandlordRepository = GetDefaultILandlordRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            string email = "email";

            Plan plan = new Plan
            {
                Id = 1

            };

            Landlord landlord1 = new Landlord
            {
                IdUser = 1,
                Email = email

            };

            Landlord landlord2 = new Landlord
            {
                IdUser = 2,
                Email = "email2"

            };

            List<User> users = new List<User>();
            //:(
            users.Add(landlord1);

            mockPlanRepository.Setup(u => u.AddAsync(plan)).Returns(Task.FromResult<Plan>(plan));
            mockPlanRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Plan>(plan));

            mockUserRepository.Setup(u => u.ListAsync()).Returns(Task.FromResult<IEnumerable<User>>(users as IEnumerable<User>));

            mockLandlordRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Landlord>(landlord1));
            mockLandlordRepository.Setup(u => u.AddAsync(landlord2)).Returns(Task.FromResult<Landlord>(null));

            var service = new LandlordService(mockLandlordRepository.Object, mockUnitOfWork.Object, mockPlanRepository.Object, mockPostRepository.Object, mockUserRepository.Object);


            // Act

            LandlordResponse result = await service.SaveAsync(landlord2, 1);


            // Assert

            result.Resource.Should().Be(landlord2);
        }


        [Test]
        public async Task SaveLandlordWhenUserIsYoungerThanRequiredReturnsCantSave()
        {
            // Arrange

            var mockLandlordRepository = GetDefaultILandlordRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            DateTime birthday = new DateTime(2005,06,10);


            Plan plan = new Plan
            {
                Id = 1

            };

            Landlord landlord1 = new Landlord
            {
                IdUser = 1,
                Birthday = birthday

            };

        
            List<User> users = new List<User>();
            //:(
            users.Add(landlord1);

            mockPlanRepository.Setup(u => u.AddAsync(plan)).Returns(Task.FromResult<Plan>(plan));
            mockPlanRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Plan>(plan));

            mockUserRepository.Setup(u => u.ListAsync()).Returns(Task.FromResult<IEnumerable<User>>(users as IEnumerable<User>));

            mockLandlordRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Landlord>(landlord1));

            var service = new LandlordService(mockLandlordRepository.Object, mockUnitOfWork.Object, mockPlanRepository.Object, mockPostRepository.Object, mockUserRepository.Object);


            // Act


            LandlordResponse result = await service.SaveAsync(landlord1, 1);
            var message = result.Message;


            // Assert

            message.Should().Be("El Landlord debe ser mayor de 18 años");
        }


        [Test]
        public async Task SaveLandlordWhenUserIsOlderThanEighteenReturnsSave()
        {
            // Arrange

            var mockLandlordRepository = GetDefaultILandlordRepositoryInstance();
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();

            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            DateTime birthday = new DateTime(1990, 06, 10);


            Plan plan = new Plan
            {
                Id = 1

            };

            Landlord landlord1 = new Landlord
            {
                IdUser = 1,
                Birthday = birthday

            };


            List<User> users = new List<User>();
            //:(
           // users.Add(landlord1);

            mockPlanRepository.Setup(u => u.AddAsync(plan)).Returns(Task.FromResult<Plan>(plan));
            mockPlanRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Plan>(plan));

           // mockUserRepository.Setup(u => u.ListAsync()).Returns(Task.FromResult<IEnumerable<User>>(users as IEnumerable<User>));


            mockLandlordRepository.Setup(u => u.AddAsync(landlord1)).Returns(Task.FromResult<Landlord>(null));
            mockLandlordRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Landlord>(landlord1));

            var service = new LandlordService(mockLandlordRepository.Object, mockUnitOfWork.Object, mockPlanRepository.Object, mockPostRepository.Object, mockUserRepository.Object);


            // Act


            LandlordResponse result = await service.SaveAsync(landlord1, 1);
           

            // Assert

            result.Resource.Should().Be(landlord1);
        }

        [Test]
        public async Task GetAllAsyncWhenNoLandlordReturnsEmptyCollection()
        {
            // Arrange

            var mockLandlordRepository = GetDefaultILandlordRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            mockLandlordRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Landlord>());

            var service = new LandlordService(mockLandlordRepository.Object, mockUnitOfWork.Object);

            // Act

            List<Landlord> result = (List<Landlord>)await service.ListAsync();
            var landlordCount = result.Count;

            // Assert

            landlordCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsLandlordNotFoundResponse()
        {
            // Arrange
            var mockLandlordRepository = GetDefaultILandlordRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var landlordId = 1;
            Landlord landlord = new Landlord();
            mockLandlordRepository.Setup(r => r.FindById(landlordId)).Returns(Task.FromResult<Landlord>(null));
            var service = new LandlordService(mockLandlordRepository.Object, mockUnitOfWork.Object);

            // Act
            LandlordResponse result = await service.GetByIdAsync(landlordId);
            var message = result.Message;

            // Assert
            message.Should().Be("Arrendador inexistente");
        }

        private Mock<ILandlordRepository> GetDefaultILandlordRepositoryInstance()
        {
            return new Mock<ILandlordRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }

        private Mock<IPlanRepository> GetDefaultIPlanRepositoryInstance()
        {
            return new Mock<IPlanRepository>();
        }

        private Mock<IPostRepository> GetDefaultIPostRepositoryInstance()
        {
            return new Mock<IPostRepository>();
        }
    }
}*/