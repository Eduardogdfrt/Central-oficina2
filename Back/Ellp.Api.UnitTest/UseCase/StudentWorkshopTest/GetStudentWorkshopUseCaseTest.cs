using System;
using System.Threading;
using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetStudentWorkshop;
using Ellp.Api.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ellp.Api.Tests.UseCases.StudentWorkshop
{
    public class GetStudentWorkshopUseCaseTests
    {
        private readonly Mock<IStudentWorkshopRepository> _studentWorkshopRepositoryMock;
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly Mock<IWorkshopRepository> _workshopRepositoryMock;
        private readonly Mock<ILogger<GetStudentWorkshopUseCase>> _loggerMock;
        private readonly GetStudentWorkshopUseCase _useCase;

        public GetStudentWorkshopUseCaseTests()
        {
            _studentWorkshopRepositoryMock = new Mock<IStudentWorkshopRepository>();
            _studentRepositoryMock = new Mock<IStudentRepository>();
            _workshopRepositoryMock = new Mock<IWorkshopRepository>();
            _loggerMock = new Mock<ILogger<GetStudentWorkshopUseCase>>();
            _useCase = new GetStudentWorkshopUseCase(
                _studentWorkshopRepositoryMock.Object,
                _studentRepositoryMock.Object,
                _workshopRepositoryMock.Object,
                _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenStudentWorkshopIsFound()
        {
            // Arrange
            var request = new GetStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            var studentWorkshop = new WorkshopAluno { StudentId = 1, WorkshopId = 1 };
            var student = new Student(1, "John Doe", "john.doe@example.com", "password", DateTime.Now, true);
            var workshop = new Workshop { Id = 1, Name = "Workshop 1" };

            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(studentWorkshop);
            _studentRepositoryMock
                .Setup(repo => repo.GetStudentByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(student);
            _workshopRepositoryMock
                .Setup(repo => repo.GetWorkshopByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(workshop);

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Relação encontrada", result.Message);
            Assert.Equal(studentWorkshop, result.Data);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
            _studentRepositoryMock.Verify(repo => repo.GetStudentByIdAsync(request.StudentId), Times.Once);
            _workshopRepositoryMock.Verify(repo => repo.GetWorkshopByIdAsync(request.WorkshopId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenStudentWorkshopNotFound()
        {
            // Arrange
            var request = new GetStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((WorkshopAluno)null);

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Relação entre aluno e workshop não encontrada", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
            _studentRepositoryMock.Verify(repo => repo.GetStudentByIdAsync(It.IsAny<int>()), Times.Never);
            _workshopRepositoryMock.Verify(repo => repo.GetWorkshopByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new GetStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Ocorreu um erro ao buscar a relação entre o aluno e o workshop", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
            _studentRepositoryMock.Verify(repo => repo.GetStudentByIdAsync(It.IsAny<int>()), Times.Never);
            _workshopRepositoryMock.Verify(repo => repo.GetWorkshopByIdAsync(It.IsAny<int>()), Times.Never);
        }
    }
}




