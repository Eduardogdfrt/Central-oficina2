using System.Threading.Tasks;
using Ellp.Api.Application.UseCases.StudentWorkshop.AddStundentWorkshop;
using Ellp.Api.Application.UseCases.StudentWorkshop.DeleteUserForWorkShop;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetStudentWorkshop;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentWorkshops;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentsForWorkshop;
using Ellp.Api.Application.UseCases.StudentWorkshop.EmitirCertificadosEmLote;
using Ellp.Api.Application.UseCases.StudentWorkshop.GetCertification;
using Ellp.Api.Webapi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Tests.Controllers
{
    public class WorkshopStudentControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly WorkshopStudentController _controller;

        public WorkshopStudentControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new WorkshopStudentController(_mediatorMock.Object);
        }

        [Fact]
        public async Task AddStudentWorkshop_ShouldReturnCreated_WhenSuccess()
        {
            // Arrange
            var input = new AddStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            var output = new AddStudentWorkshopOutput { Success = true, Message = "Workshop adicionado ao estudante com sucesso" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddStudentWorkshopInput>(), default)).ReturnsAsync(output);

            // Act
            var result = await _controller.AddStudentWorkshop(input);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.Equal(output, createdResult.Value);
        }

        [Fact]
        public async Task DeleteStudentWorkshop_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var input = new DeleteStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            var output = new DeleteStudentWorkshopOutput { Success = true, Message = "Aluno removido do workshop com sucesso" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteStudentWorkshopInput>(), default)).ReturnsAsync(output);

            // Act
            var result = await _controller.DeleteStudentWorkshop(input);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(output, okResult.Value);
        }

        [Fact]
        public async Task GetStudentWorkshop_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var input = new GetStudentWorkshopInput { StudentId = 1, WorkshopId = 1 };
            var output = new GetStudentWorkshopOutput { Success = true, Message = "Relação encontrada", Data = new WorkshopAluno() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetStudentWorkshopInput>(), default)).ReturnsAsync(output);

            // Act
            var result = await _controller.GetStudentWorkshop(1, 1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(output, okResult.Value);
        }

        [Fact]
        public async Task GetAllStudentWorkshops_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var input = new GetAllStudentWorkshopsInput { StudentId = 1 };
            var output = new GetAllStudentWorkshopsOutput { Success = true, Message = "Workshops encontrados", Workshops = new List<Workshop>() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllStudentWorkshopsInput>(), default)).ReturnsAsync(output);

            // Act
            var result = await _controller.GetAllStudentWorkshops(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(output, okResult.Value);
        }

        [Fact]
        public async Task GetAllStudentsForWorkshop_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var input = new GetAllStudentsForWorkshopInput { WorkshopId = 1 };
            var output = new GetAllStudentsForWorkshopOutput { Success = true, Message = "Alunos encontrados", Students = new List<Student>() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllStudentsForWorkshopInput>(), default)).ReturnsAsync(output);

            // Act
            var result = await _controller.GetAllStudentsForWorkshop(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(output, okResult.Value);
        }

        [Fact]
        public async Task EmitirCertificado_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var input = new EmitirCertificadosEmLoteInput { Certificados = new List<CertificadoRequest> { new CertificadoRequest { StudentId = 1, WorkshopId = 1 } } };
            var output = new EmitirCertificadosEmLoteOutput { Success = true, Message = "Certificados emitidos com sucesso", HashCodes = new List<string> { "hash1" } };
            _mediatorMock.Setup(m => m.Send(It.IsAny<EmitirCertificadosEmLoteInput>(), default)).ReturnsAsync(output);

            // Act
            var result = await _controller.EmitirCertificado(input);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(output, okResult.Value);
        }

        [Fact]
        public async Task GetCertification_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var input = new GetCertificationInput { StudentId = 1, WorkshopId = 1 };
            var output = new GetCertificationOutput { Success = true, Message = "Certificação encontrada", Certificate = "Certificado123" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCertificationInput>(), default)).ReturnsAsync(output);

            // Act
            var result = await _controller.GetCertification(1, 1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(output, okResult.Value);
        }
    }
}
