using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using MediatR;
using Ellp.Api.WebApi.Controllers;
using Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginStudent;
using Ellp.Api.Application.UseCases.AddParticipantUsecases.AddNewStudentUseCases;

namespace Ellp.Api.UnitTest.Controllers
{
    public class StudentControllerTests
    {
        private readonly Mock<ILogger<StudentController>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly StudentController _controller;

        public StudentControllerTests()
        {
            _loggerMock = new Mock<ILogger<StudentController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new StudentController(_loggerMock.Object, _mediatorMock.Object);
        }

        #region GetLoginStudent Tests

        [Fact]
        public async Task GetLoginStudent_ShouldReturnOk_WhenLoginIsSuccessful()
        {
            // Arrange
            var email = "student@example.com";
            var password = "correctPassword";
            var input = new GetLoginStudentInput { Email = email, Password = password };
            var result = new GetLoginStudentMapper
            {
                Success = true,
                StudentId = 1,
                Email = email,
                Name = "Jane Doe"
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetLoginStudentInput>(i => i.Email == email && i.Password == password), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var response = await _controller.GetLoginStudent(email, password, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(result, okResult.Value);
        }

        [Fact]
        public async Task GetLoginStudent_ShouldReturnBadRequest_WhenLoginFails()
        {
            // Arrange
            var email = "student@example.com";
            var password = "wrongPassword";
            var input = new GetLoginStudentInput { Email = email, Password = password };
            var result = new GetLoginStudentMapper
            {
                Success = false,
                Message = "Email ou senha inválidos"
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetLoginStudentInput>(i => i.Email == email && i.Password == password), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var response = await _controller.GetLoginStudent(email, password, CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            var responseValue = Assert.IsType<Response>(badRequestResult.Value);
            Assert.Equal("Email ou senha inválidos", responseValue.Message);
        }

        [Fact]
        public async Task GetLoginStudent_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            var email = "student@example.com";
            var password = "anyPassword";

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetLoginStudentInput>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await _controller.GetLoginStudent(email, password, CancellationToken.None);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

            var responseValue = Assert.IsType<Response>(objectResult.Value);
            Assert.Equal("Ocorreu um erro durante o processamento", responseValue.Message);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Ocorreu um erro ao processar a solicitação de login.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        #endregion

    }
}
