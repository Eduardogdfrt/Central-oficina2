using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Moq;
using Xunit;
using Ellp.Api.Application.Utilities;
using Ellp.Api.WebApi.Controllers;
using Ellp.Api.Application.UseCases.Workshops.AddWorkshops;

namespace Ellp.Api.UnitTest.Controllers
{
    public class WorkshopControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly WorkshopController _controller;

        public WorkshopControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new WorkshopController(_mediatorMock.Object);
        }

        [Fact]
        public async Task AddWorkshop_ShouldReturnOk_WhenWorkshopIsCreatedSuccessfully()
        {
            // Arrange
            var input = new AddWorkshopInput
            {
                Name = "Workshop de Inteligência Artificial",
                Data = new DateTime(2024, 5, 20)
            };

            var response = new Ellp.Api.Application.Utilities.Response { Message = "Workshop criado com sucesso" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AddWorkshopInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.AddWorkshop(input);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            var returnedResponse = Assert.IsType<Ellp.Api.Application.Utilities.Response>(okResult.Value);
            Assert.Equal("Workshop criado com sucesso", returnedResponse.Message);

            _mediatorMock.Verify(m => m.Send(It.Is<AddWorkshopInput>(i => i.Name == input.Name && i.Data == input.Data), It.IsAny<CancellationToken>()), Times.Once);
        }

    
    }
}
