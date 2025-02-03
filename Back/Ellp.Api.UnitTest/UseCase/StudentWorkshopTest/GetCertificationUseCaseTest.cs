using System;
using System.Threading;
using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetCertification;
using Ellp.Api.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ellp.Api.Tests.UseCases.StudentWorkshop
{
    public class GetCertificationUseCaseTests
    {
        private readonly Mock<IStudentWorkshopRepository> _studentWorkshopRepositoryMock;
        private readonly Mock<ILogger<GetCertificationUseCase>> _loggerMock;
        private readonly GetCertificationUseCase _useCase;

        public GetCertificationUseCaseTests()
        {
            _studentWorkshopRepositoryMock = new Mock<IStudentWorkshopRepository>();
            _loggerMock = new Mock<ILogger<GetCertificationUseCase>>();
            _useCase = new GetCertificationUseCase(_studentWorkshopRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCertificationIsFound()
        {
            // Arrange
            var request = new GetCertificationInput { StudentId = 1, WorkshopId = 1 };
            var studentWorkshop = new WorkshopAluno { StudentId = 1, WorkshopId = 1, Certificate = "Certificado123" };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(studentWorkshop);

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Certificação encontrada", result.Message);
            Assert.Equal("Certificado123", result.Certificate);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCertificationIsNotFound()
        {
            // Arrange
            var request = new GetCertificationInput { StudentId = 1, WorkshopId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((WorkshopAluno)null);

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Certificação não encontrada", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new GetCertificationInput { StudentId = 1, WorkshopId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetStudentWorkshopAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Ocorreu um erro ao buscar a certificação", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetStudentWorkshopAsync(request.StudentId, request.WorkshopId), Times.Once);
        }
    }
}




