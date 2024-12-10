using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Utilities;
using Ellp.Api.Application.UseCases.Workshops.AddWorkshops;

namespace Ellp.Api.UnitTest.UseCase.WorkshopTest
{
    public class AddWorkshopUseCaseTests
    {
        private readonly Mock<ILogger<AddWorkshopUseCase>> _loggerMock;
        private readonly Mock<IWorkshopRepository> _workshopRepositoryMock;
        private readonly AddWorkshopUseCase _useCase;

        public AddWorkshopUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<AddWorkshopUseCase>>();
            _workshopRepositoryMock = new Mock<IWorkshopRepository>();
            _useCase = new AddWorkshopUseCase(_loggerMock.Object, _workshopRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResponse_WhenWorkshopIsAddedSuccessfully()
        {
            // Arrange
            var input = new AddWorkshopInput
            {
                Name = "Workshop de Inteligência Artificial",
                Data = new DateTime(2024, 5, 20)
            };

            _workshopRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Workshop>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.Handle(input, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Workshop criado com sucesso", result.Message);

            _workshopRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Workshop>(
                w => w.Name == input.Name &&
                     w.Data == input.Data)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnErrorResponse_WhenExceptionIsThrown()
        {
            // Arrange
            var input = new AddWorkshopInput
            {
                Name = "Workshop de Desenvolvimento Web",
                Data = new DateTime(2024, 6, 15)
            };

            _workshopRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Workshop>()))
                .ThrowsAsync(new Exception("Erro no banco de dados"));

            // Act
            var result = await _useCase.Handle(input, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ocorreu um erro durante o processamento", result.Message);

            _workshopRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Workshop>()), Times.Once);

            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Ocorreu um erro ao adicionar um novo workshop.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
