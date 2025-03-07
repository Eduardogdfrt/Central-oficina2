﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Utilities;
using Ellp.Api.Application.UseCases.Users.AddParticipantUsecases.AddNewProfessorUseCases;

namespace Ellp.Api.UnitTest.UseCases.AddParticipantUsecases.AddNewProfessorUseCases
{
    public class AddNewProfessorUseCaseTests
    {
        private readonly Mock<ILogger<AddNewProfessorUseCase>> _loggerMock;
        private readonly Mock<IProfessorRepository> _professorRepositoryMock;
        private readonly AddNewProfessorUseCase _useCase;

        public AddNewProfessorUseCaseTests()
        {
            _loggerMock = new Mock<ILogger<AddNewProfessorUseCase>>();
            _professorRepositoryMock = new Mock<IProfessorRepository>();
            _useCase = new AddNewProfessorUseCase(_loggerMock.Object, _professorRepositoryMock.Object);
        }
        //[Fact]
        //public async Task Handle_ShouldReturnSuccessResponse_WhenProfessorIsAddedSuccessfully()
        //{
        //    // Arrange
        //    var input = new AddNewProfessorInput
        //    {
        //        ProfessorId = 12345678,
        //        Name = "Alice Smith",
        //        Email = "alice.smith@example.com",
        //        Password = "SecurePass456",
        //        Specialty = "Mathematics"
        //    };

        //    _professorRepositoryMock
        //        .Setup(repo => repo.GetByEmailAsync(input.Email))
        //        .ReturnsAsync((Professor)null);

        //    _professorRepositoryMock
        //        .Setup(repo => repo.AddNewProfessorAsync(It.IsAny<Professor>()))
        //        .Returns(Task.CompletedTask);

        //    // Act
        //    var result = await _useCase.Handle(input, CancellationToken.None);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal("Professor criado com sucesso", result.Message);

        //    _professorRepositoryMock.Verify(repo => repo.GetByEmailAsync(input.Email), Times.Once);
        //    _professorRepositoryMock.Verify(repo => repo.AddNewProfessorAsync(It.Is<Professor>(
        //        p => p.ProfessorId == input.ProfessorId &&
        //             p.Name == input.Name &&
        //             p.Email == input.Email &&
        //             p.Specialty == input.Specialty
        //    )), Times.Once);
        //}


        [Fact]
        public async Task Handle_ShouldReturnFailureResponse_WhenEmailIsAlreadyInUse()
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

            var existingProfessor = new Professor(
                professorId: 87654321,
                name: "Bob Johnson",
                specialty: "Physics",
                password: "hashedpassword",
                email: "alice.smith@example.com" // Mesmo email
            );

            _professorRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(input.Email))
                .ReturnsAsync(existingProfessor); // Email já está em uso

            // Act
            var result = await _useCase.Handle(input, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Email já está em uso", result.Message);

            _professorRepositoryMock.Verify(repo => repo.GetByEmailAsync(input.Email), Times.Once);
            _professorRepositoryMock.Verify(repo => repo.AddNewProfessorAsync(It.IsAny<Professor>()), Times.Never);
        }
    }
}
