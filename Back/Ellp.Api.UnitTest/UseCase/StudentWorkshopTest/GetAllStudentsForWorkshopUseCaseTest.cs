using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentsForWorkshop;
using Ellp.Api.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ellp.Api.Tests.UseCases.StudentWorkshop
{
    public class GetAllStudentsForWorkshopUseCaseTests
    {
        private readonly Mock<IStudentWorkshopRepository> _studentWorkshopRepositoryMock;
        private readonly Mock<ILogger<GetAllStudentsForWorkshopUseCase>> _loggerMock;
        private readonly GetAllStudentsForWorkshopUseCase _useCase;

        public GetAllStudentsForWorkshopUseCaseTests()
        {
            _studentWorkshopRepositoryMock = new Mock<IStudentWorkshopRepository>();
            _loggerMock = new Mock<ILogger<GetAllStudentsForWorkshopUseCase>>();
            _useCase = new GetAllStudentsForWorkshopUseCase(_studentWorkshopRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenStudentsAreFound()
        {
            // Arrange
            var request = new GetAllStudentsForWorkshopInput { WorkshopId = 1 };
            var students = new List<Student> { new Student(1, "John Doe", "john.doe@example.com", "password", DateTime.Now, true) };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetAllStudentsByWorkshopIdAsync(It.IsAny<int>()))
                .ReturnsAsync(students);

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Alunos encontrados", result.Message);
            Assert.Equal(students, result.Students);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetAllStudentsByWorkshopIdAsync(request.WorkshopId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new GetAllStudentsForWorkshopInput { WorkshopId = 1 };
            _studentWorkshopRepositoryMock
                .Setup(repo => repo.GetAllStudentsByWorkshopIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Ocorreu um erro ao buscar os alunos do workshop", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.GetAllStudentsByWorkshopIdAsync(request.WorkshopId), Times.Once);
        }
    }
}





