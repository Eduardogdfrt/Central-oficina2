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
using Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginStudent;
using AppResponse = Ellp.Api.Application.Utilities.Response;
using Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewStudentUseCases;



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
            var result = new GetLoginStudentOutput
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
            var result = new GetLoginStudentOutput
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


        #endregion

        #region AddNewStudent Tests
        [Fact]
        public async Task AddNewStudent_ShouldReturnCreated_WhenStudentIsAddedSuccessfully()
        {
            // Arrange
            var input = new AddNewStudentInput
            {
                Name = "John Smith",
                Email = "john.smith@example.com",
                Password = "SecurePassword123",
                BirthDate = new DateTime(1998, 4, 23)
            };

            var response = new AppResponse { Message = "Estudante criado com sucesso" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AddNewStudentInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response); // Retorna AppResponse

            // Act
            var result = await _controller.AddNewStudent(input, CancellationToken.None);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.Equal(response, createdAtActionResult.Value);
            Assert.Equal(nameof(StudentController.GetLoginStudent), createdAtActionResult.ActionName);
            Assert.NotNull(createdAtActionResult.RouteValues);
            Assert.Equal(input.Email, createdAtActionResult.RouteValues["email"]);
        }

        [Fact]
        public async Task AddNewStudent_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            var input = new AddNewStudentInput
            {
                Name = "John Smith",
                Email = "john.smith@example.com",
                Password = "SecurePassword123",
                BirthDate = new DateTime(1998, 4, 23)
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AddNewStudentInput>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.AddNewStudent(input, CancellationToken.None);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);

            var responseValue = Assert.IsType<AppResponse>(objectResult.Value);
            Assert.Equal("Ocorreu um erro durante o processamento", responseValue.Message);

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Ocorreu um erro ao adicionar um novo estudante.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        #endregion
    }
}


