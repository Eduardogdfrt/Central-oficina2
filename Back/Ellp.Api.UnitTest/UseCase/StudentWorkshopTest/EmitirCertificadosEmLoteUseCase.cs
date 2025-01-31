using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Application.UseCases.StudentWorkshop.EmitirCertificadosEmLote;
using Ellp.Api.Application.Utilities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ellp.Api.Tests.UseCases.StudentWorkshop
{
    public class EmitirCertificadosEmLoteUseCaseTests
    {
        private readonly Mock<IStudentWorkshopRepository> _studentWorkshopRepositoryMock;
        private readonly Mock<ILogger<EmitirCertificadosEmLoteUseCase>> _loggerMock;
        private readonly EmitirCertificadosEmLoteUseCase _useCase;

        public EmitirCertificadosEmLoteUseCaseTests()
        {
            _studentWorkshopRepositoryMock = new Mock<IStudentWorkshopRepository>();
            _loggerMock = new Mock<ILogger<EmitirCertificadosEmLoteUseCase>>();
            _useCase = new EmitirCertificadosEmLoteUseCase(_studentWorkshopRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCertificatesAreIssued()
        {
            // Arrange
            var request = new EmitirCertificadosEmLoteInput
            {
                Certificados = new List<CertificadoRequest>
                {
                    new CertificadoRequest { StudentId = 1, WorkshopId = 1 },
                    new CertificadoRequest { StudentId = 2, WorkshopId = 2 }
                }
            };

            _studentWorkshopRepositoryMock
                .Setup(repo => repo.EmitirCertificadoAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Certificados emitidos com sucesso", result.Message);
            Assert.Equal(2, result.HashCodes.Count);
            _studentWorkshopRepositoryMock.Verify(repo => repo.EmitirCertificadoAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var request = new EmitirCertificadosEmLoteInput
            {
                Certificados = new List<CertificadoRequest>
                {
                    new CertificadoRequest { StudentId = 1, WorkshopId = 1 }
                }
            };

            _studentWorkshopRepositoryMock
                .Setup(repo => repo.EmitirCertificadoAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _useCase.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Ocorreu um erro ao emitir os certificados", result.Message);
            _studentWorkshopRepositoryMock.Verify(repo => repo.EmitirCertificadoAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }
    }
}
