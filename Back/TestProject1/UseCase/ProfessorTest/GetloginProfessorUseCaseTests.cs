using Moq;
using Microsoft.Extensions.Logging;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using Xunit;
using Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginProfessor;

namespace Ellp.Api.UnitTest.UseCase.ProfessorTest
{
    public class GetLoginProfessorUseCaseTests
    {
        private readonly Mock<ILogger<GetLoginProfessorUseCase>> _loggerMock;
        private readonly Mock<IProfessorRepository> _professorRepositoryMock;
        private readonly GetLoginProfessorUseCase _useCase;

        public GetLoginProfessorUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<GetLoginProfessorUseCase>>();
            _professorRepositoryMock = new Mock<IProfessorRepository>();
            _useCase = new GetLoginProfessorUseCase(_loggerMock.Object, _professorRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenProfessorExists()
        {
            // Arrange
            var professorId = 1;
            var password = "password";
            var professor = new Professor(professorId, "John Doe", "Math", password, "john.doe@example.com");

            _professorRepositoryMock
                .Setup(repo => repo.GetAllProfessorInfosAsync(professorId, password))
                .ReturnsAsync(professor);

            var request = new GetLoginProfessorInput { ProfessorId = professorId, Password = password };

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(professorId, result.ProfessorId);
            Assert.Equal(professor.Email, result.Email);
            Assert.Equal(professor.Name, result.Name);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenProfessorDoesNotExist()
        {
            // Arrange
            var professorId = 1;
            var password = "wrongpassword";

            _professorRepositoryMock
                .Setup(repo => repo.GetAllProfessorInfosAsync(professorId, password))
                .ReturnsAsync((Professor)null);

            var request = new GetLoginProfessorInput { ProfessorId = professorId, Password = password };

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid professor ID or password", result.Message);
        }
    }
}
