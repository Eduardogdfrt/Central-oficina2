using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.AddStundentWorkshop
{
    public class AddStudentWorkshopInput : IRequest<AddStudentWorkshopOutput>
    {
        public int StudentId { get; set; }
        public int WorkshopId { get; set; }
    }
}