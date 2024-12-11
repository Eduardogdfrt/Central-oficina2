using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Moq;
using Xunit;
using Ellp.Api.WebApi.Controllers;
using Ellp.Api.Application.UseCases.Workshops.AddWorkshops;
using Ellp.Api.Application.UseCases.Workshops.GetWorkshopById;
using Microsoft.AspNetCore.Http;

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
        public async Task AddWorkshop_ShouldReturnCreated_WhenWorkshopIsCreatedSuccessfully()
        {
            // Arrange
            var input = new AddWorkshopInput
            {
                Name = "Workshop de Inteligência Artificial",
                Data = new DateTime(2024, 5, 20)
            };

            var response = new AddWorkshopOutput
            {
                Id = 1,
                Name = input.Name,
                Message = "Workshop criado com sucesso"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<AddWorkshopInput>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.AddWorkshop(input);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdAtActionResult.StatusCode);
            Assert.Equal(response, createdAtActionResult.Value);
            Assert.Equal(nameof(WorkshopController.GetWorkshopById), createdAtActionResult.ActionName);
            Assert.NotNull(createdAtActionResult.RouteValues);
            Assert.Equal(response.Id, createdAtActionResult.RouteValues["id"]);

            _mediatorMock.Verify(m => m.Send(It.Is<AddWorkshopInput>(i => i.Name == input.Name && i.Data == input.Data), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetWorkshopById_ShouldReturnNotFound_WhenWorkshopIsNotFound()
        {
            // Arrange
            var workshopId = 1;
            var result = new GetWorkshopByIdOutput { Success = false };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetWorkshopByIdInput>(i => i.Id == workshopId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            // Act
            var response = await _controller.GetWorkshopById(workshopId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.Equal(result, notFoundResult.Value);
        }
    }
}





