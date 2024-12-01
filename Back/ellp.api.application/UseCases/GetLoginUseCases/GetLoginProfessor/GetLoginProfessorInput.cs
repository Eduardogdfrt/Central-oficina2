using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ellp.Api.Application.UseCases.GetLoginUseCases.GetLoginProfessor
{
    public class GetLoginProfessorInput : IRequest<GetLoginProfessorMapper>
    {
        public int ProfessorId { get; set; }
        public string? Password { get; set; }
    }
}
