using System;
using System.Collections.Generic;
using MediatR;

namespace Ellp.Api.Application.UseCases.Users.SearchAllProfessors
{
    public class SearchAllProfessorsInput : IRequest<IEnumerable<SearchAllProfessorsOutput>>
    {
        public int? ProfessorId { get; set; }

        public SearchAllProfessorsInput(int? professorId = null)
        {
            ProfessorId = professorId;
        }
    }
}
