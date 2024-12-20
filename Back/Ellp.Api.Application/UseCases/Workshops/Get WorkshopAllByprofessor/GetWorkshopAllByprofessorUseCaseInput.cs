using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Ellp.Api.Application.UseCases.Workshops.GetWorkshopAll
{
    public class GetWorkshopAllByprofessorUseCaseInput : IRequest<GetWorkshopAllByprofessorUseCaselOutput>
    {
        public int professorId {get; set;} 

    }
}









