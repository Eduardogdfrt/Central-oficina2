using System;
using System.Threading;
using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.UseCases.StudentWorkshop.AddStundentWorkshop;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ellp.Api.Tests.UseCases.StudentWorkshop
{
    public class AddStudentWorkshopUseCaseTests
    {
        private readonly Mock<IStudentWorkshopRepository> _studentWorkshopRepositoryMock;
        private readonly Mock<ILogger<AddStudentWorkshopUseCase>> _loggerMock;
        private readonly AddStudentWorkshopUseCase _useCase;

        public AddStudentWorkshopUseCaseTests()
        {
            _studentWorkshopRepositoryMock = new Mock<IStudentWorkshopRepository>();
            _loggerMock = new Mock<ILogger<AddStudentWorkshopUseCase>>();
            _useCase = new AddStudentWorkshopUseCase(_loggerMock.Object, _studentWorkshopRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenWorkshopIsAdded()
        {
            // Arrange
            var request = new AddStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.AddStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Workshop adicionado ao estudante com sucesso", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.AddStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new AddStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.AddStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Ocorreu um erro ao adicionar o workshop ao estudante", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.AddStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
        }
    }
}
