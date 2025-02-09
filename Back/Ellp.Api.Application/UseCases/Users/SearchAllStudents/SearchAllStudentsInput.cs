using System;
using System.Collections.Generic;
using MediatR;

namespace Ellp.Api.Application.UseCases.Users.SearchAllStudents
{
    public class SearchAllStudentsInput : IRequest<IEnumerable<SearchAllStudentsOutput>>
    {
        public int? StudentId { get; set; }

        public SearchAllStudentsInput(int? studentId = null)
        {
            StudentId = studentId;
        }
    }
}

