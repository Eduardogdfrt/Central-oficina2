using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginStudent;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.UnitTest.UseCase.StudentsTests
{
    public class GetLoginStudentUseCaseTests
    {
        private readonly Mock<ILogger<GetLoginStudentUseCase>> _loggerMock;
        private readonly Mock<IStudentRepository> _studentRepositoryMock;
        private readonly GetLoginStudentUseCase _useCase;

        public GetLoginStudentUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<GetLoginStudentUseCase>>();
            _studentRepositoryMock = new Mock<IStudentRepository>();
            _useCase = new GetLoginStudentUseCase(_loggerMock.Object, _studentRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenStudentNotFound()
        {
            // Arrange
            var input = new GetLoginStudentInput
            {
                Email = "nonexistent@example.com",
                Password = "anyPassword"
            };

            _studentRepositoryMock
                .Setup(repo => repo.GetStudentByEmailAsync(input.Email))
                .ReturnsAsync((Student)null);

            // Act
            var result = await _useCase.Handle(input, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Email inválido ou não autenticado", result.Message);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenStudentIsAuthenticatedButPasswordIsMissing()
        {
            // Arrange
            var input = new GetLoginStudentInput
            {
                Email = "student@example.com",
                Password = null
            };

            var student = new Student(
                id: 1,
                name: "Jane Doe",
                email: "student@example.com",
                password: "hashedPassword",
                BirthDate: new DateTime(2000, 1, 1),
                IsAuthenticated: true
            );

            _studentRepositoryMock
                .Setup(repo => repo.GetStudentByEmailAsync(input.Email))
                .ReturnsAsync(student);

            // Act
            var result = await _useCase.Handle(input, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Senha é obrigatória para login", result.Message);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenPasswordIsIncorrect()
        {
            // Arrange
            var input = new GetLoginStudentInput
            {
                Email = "student@example.com",
                Password = "wrongPassword"
            };

            var student = new Student(
                id: 1,
                name: "Jane Doe",
                email: "student@example.com",
                password: "correctPassword",
                BirthDate: new DateTime(2000, 1, 1),
                IsAuthenticated: true
            );

            _studentRepositoryMock
                .Setup(repo => repo.GetStudentByEmailAsync(input.Email))
                .ReturnsAsync(student);

            _studentRepositoryMock
                .Setup(repo => repo.GetStudentByEmailAndPasswordAsync(input.Email, input.Password))
                .ReturnsAsync((Student)null);

            // Act
            var result = await _useCase.Handle(input, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Email ou senha inválidos", result.Message);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenStudentIsAuthenticatedAndPasswordIsCorrect()
        {
            // Arrange
            var input = new GetLoginStudentInput
            {
                Email = "student@example.com",
                Password = "correctPassword"
            };

            var student = new Student(
                id: 1,
                name: "Jane Doe",
                email: "student@example.com",
                password: "correctPassword",
                BirthDate: new DateTime(2000, 1, 1),
                IsAuthenticated: true
            );

            _studentRepositoryMock
                .Setup(repo => repo.GetStudentByEmailAsync(input.Email))
                .ReturnsAsync(student);

            _studentRepositoryMock
                .Setup(repo => repo.GetStudentByEmailAndPasswordAsync(input.Email, input.Password))
                .ReturnsAsync(student);

            // Act
            var result = await _useCase.Handle(input, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(student.Id, result.StudentId);
            Assert.Equal(student.Email, result.Email);
            Assert.Equal(student.Name, result.Name);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenStudentIsNotAuthenticated()
        {
            // Arrange
            var input = new GetLoginStudentInput
            {
                Email = "student@example.com",
                Password = "anyPassword"
            };

            var student = new Student(
                id: 2,
                name: "John Smith",
                email: "student@example.com",
                password: "anyPassword",
                BirthDate: new DateTime(1999, 5, 15),
                IsAuthenticated: false
            );

            _studentRepositoryMock
                .Setup(repo => repo.GetStudentByEmailAsync(input.Email))
                .ReturnsAsync(student);

            // Act
            var result = await _useCase.Handle(input, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(student.Id, result.StudentId);
            Assert.Equal(student.Email, result.Email);
            Assert.Equal(student.Name, result.Name);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var input = new GetLoginStudentInput
            {
                Email = "student@example.com",
                Password = "anyPassword"
            };

            _studentRepositoryMock
                .Setup(repo => repo.GetStudentByEmailAsync(input.Email))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _useCase.Handle(input, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Ocorreu um erro durante o processamento", result.Message);

            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Ocorreu um erro ao processar a solicitação de login.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
