using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Ellp.Api.Application.UseCases.Workshops.GetWorkshopAll
{
    public class GetWorkshopAllByprofessorInput : IRequest<GetWorkshopAllByprofessorOutput>
    {
        public int professorId {get; set;} 

    }
}









