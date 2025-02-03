using System;
using System.Threading;
using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.UseCases.StudentWorkshop.DeleteUserForWorkShop;
using Ellp.Api.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ellp.Api.Tests.UseCases.StudentWorkshop
{
    public class DeleteStudentWorkshopUseCaseTests
    {
        private readonly Mock<IStudentWorkshopRepository> _studentWorkshopRepositoryMock;
        private readonly Mock<ILogger<DeleteStudentWorkshopUseCase>> _loggerMock;
        private readonly DeleteStudentWorkshopUseCase _useCase;

        public DeleteStudentWorkshopUseCaseTests()
        {
            _studentWorkshopRepositoryMock = new Mock<IStudentWorkshopRepository>();
            _loggerMock = new Mock<ILogger<DeleteStudentWorkshopUseCase>>();
            _useCase = new DeleteStudentWorkshopUseCase(_studentWorkshopRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenStudentWorkshopIsDeleted()
        {
            // Arrange
            var request = new DeleteStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            var studentWorkshop = new WorkshopAluno { StudentId = 1, WorkshopId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(studentWorkshop);
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.DeleteStudentWorkshopAsync(It.IsAny<WorkshopAluno>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Aluno removido do workshop com sucesso", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
            _studentWorkshopRepositoryMock.Verify(repo => repo.DeleteStudentWorkshopAsync(studentWorkshop), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenStudentWorkshopNotFound()
        {
            // Arrange
            var request = new DeleteStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((WorkshopAluno)null);

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Relação entre aluno e workshop não encontrada", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
            _studentWorkshopRepositoryMock.Verify(repo => repo.DeleteStudentWorkshopAsync(It.IsAny<WorkshopAluno>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new DeleteStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Ocorreu um erro ao remover o aluno do workshop", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
            _studentWorkshopRepositoryMock.Verify(repo => repo.DeleteStudentWorkshopAsync(It.IsAny<WorkshopAluno>()), Times.Never);
        }
    }
}




