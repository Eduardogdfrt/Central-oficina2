using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Utilities;
using Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewStudentUseCases;

namespace Ellp.Api.UnitTest.UseCases.AddParticipantUsecases.AddNewStudentUseCases
{
    public class AddNewStudentUseCaseTests
    {
        private readonly Mock<ILogger<AddNewStudentUseCase>> _loggerMock;
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly AddNewStudentUseCase _useCase;

        public AddNewStudentUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<AddNewStudentUseCase>>();
            _studentRepositoryMock = new Mock<IStudentRepository>();
            _useCase = new AddNewStudentUseCase(_loggerMock.Object, _studentRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResponse_WhenStudentIsAddedSuccessfully()
        {
            // Arrange
            var input = new AddNewStudentInput
            {
                Name = "Carlos Oliveira",
                Email = "carlos.oliveira@example.com",
                Password = "SecurePass789",
                BirthDate = new DateTime(1995, 5, 23)
            };

            _studentRepositoryMock
                .Setup(repo => repo.GetStudentByEmailAsync(input.Email))
                .ReturnsAsync((Student)null); // Email não está em uso

            _studentRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Student>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.Handle(input, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Estudante criado com sucesso", result.Message);

            _studentRepositoryMock.Verify(repo => repo.GetStudentByEmailAsync(input.Email), Times.Once);
            _studentRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Student>(
                s => s.Name == input.Name &&
                     s.Email == input.Email &&
                     s.BirthDate == input.BirthDate &&
                     !string.IsNullOrEmpty(s.Password) &&
                     s.IsAuthenticated == false)), Times.Once);
        }
    }
}
