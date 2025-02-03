using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentWorkshops;
using Ellp.Api.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ellp.Api.Tests.UseCases.StudentWorkshop
{
    public class GetAllStudentWorkshopsUseCaseTests
    {
        private readonly Mock<IStudentWorkshopRepository> _studentWorkshopRepositoryMock;
        private readonly Mock<ILogger<GetAllStudentWorkshopsUseCase>> _loggerMock;
        private readonly GetAllStudentWorkshopsUseCase _useCase;

        public GetAllStudentWorkshopsUseCaseTests()
        {
            _studentWorkshopRepositoryMock = new Mock<IStudentWorkshopRepository>();
            _loggerMock = new Mock<ILogger<GetAllStudentWorkshopsUseCase>>();
            _useCase = new GetAllStudentWorkshopsUseCase(_studentWorkshopRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenWorkshopsAreFound()
        {
            // Arrange
            var request = new GetAllStudentWorkshopsInput { StudentId = 1 };
            var workshops = new List<Workshop> { new Workshop { Id = 1, Name = "Workshop 1" } };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetAllWorkshopsByStudentIdAsync(It.IsAny<int>()))
                .ReturnsAsync(workshops);

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Workshops encontrados", result.Message);
            Assert.Equal(workshops, result.Workshops);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetAllWorkshopsByStudentIdAsync(request.StudentId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new GetAllStudentWorkshopsInput { StudentId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetAllWorkshopsByStudentIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Ocorreu um erro ao buscar os workshops do aluno", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetAllWorkshopsByStudentIdAsync(request.StudentId), Times.Once);
        }
    }
}




