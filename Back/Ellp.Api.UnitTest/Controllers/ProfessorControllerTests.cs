using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using MediatR;
using Ellp.Api.Application.UseCases.Users.GetLoginUseCases.GetLoginProfessor;
using Ellp.Api.WebApi.Controllers;
using Ellp.Api.Application.Utilities;
using Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewProfessorUseCases;

namespace Ellp.Api.UnitTest.Controllers
{
    public class ProfessorControllerTests
    {
        private readonly Mock<ILogger<ProfessorController>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProfessorController _controller;

        public ProfessorControllerTests()
        {
            _loggerMock = new Mock<ILogger<ProfessorController>>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProfessorController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task GetLoginProfessor_ShouldReturnOk_WhenLoginIsSuccessful()
        {
            // Arrange
            var professorId = 23106789;
            var password = "Teste123";
            var input = new GetLoginProfessorInput { ProfessorId = professorId, Password = password };
            var result = new GetLoginProfessorOutput
            {
                Success = true,
                ProfessorId = professorId,
                Email = "john.doe@example.com",
                Name = "John Doe"
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetLoginProfessorInput>(i => i.ProfessorId == professorId && i.Password == password), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var response = await _controller.GetLoginProfessor(professorId, password, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(result, okResult.Value);
        }

        [Fact]
        public async Task GetLoginProfessor_ShouldReturnBadRequest_WhenLoginFails()
        {
            // Arrange
            var professorId = 1;
            var password = "wrongpassword";
            var input = new GetLoginProfessorInput { ProfessorId = professorId, Password = password };
            var result = new GetLoginProfessorOutput { Success = false, Message = "Invalid professor ID or password" };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetLoginProfessorInput>(i => i.ProfessorId == professorId && i.Password == password), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var response = await _controller.GetLoginProfessor(professorId, password, CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

           
        }

        [Fact]
        public async Task GetLoginProfessor_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            var professorId = 1;
            var password = "password";
            var input = new GetLoginProfessorInput { ProfessorId = professorId, Password = password };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetLoginProfessorInput>(i => i.ProfessorId == professorId && i.Password == password), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var response = await _controller.GetLoginProfessor(professorId, password, CancellationToken.None);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(StatusCodes.Status500InternalServerError, internalServerErrorResult.StatusCode);

         

            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString() == "Ocorreu um erro ao processar a solicitação de login."),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task AddNewProfessor_ShouldReturnCreated_WhenProfessorIsAddedSuccessfully()
        {
            // Arrange
            var input = new AddNewProfessorInput
            {
                ProfessorId = 12345678,
                Name = "Alice Smith",
                Email = "alice.smith@example.com",
                Password = "SecurePass456",
                Specialty = "Mathematics"
            };

            var response = new AddNewProfessorOutput
            {
                ProfessorId = input.ProfessorId,
                Name = input.Name,
                Password = input.Password,
                Message = "Professor criado com sucesso"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AddNewProfessorInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.AddNewProfessor(input, CancellationToken.None);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.Equal(response, createdAtActionResult.Value);
            Assert.Equal(nameof(ProfessorController.GetLoginProfessor), createdAtActionResult.ActionName);
            Assert.NotNull(createdAtActionResult.RouteValues);
            Assert.Equal(input.ProfessorId, createdAtActionResult.RouteValues["professorId"]);
        }
    }
}



