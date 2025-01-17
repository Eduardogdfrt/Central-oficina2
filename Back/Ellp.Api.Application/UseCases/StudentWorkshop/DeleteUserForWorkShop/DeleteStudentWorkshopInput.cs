using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.DeleteUserForWorkShop
{
    public class DeleteStudentWorkshopInput : IRequest<DeleteStudentWorkshopOutput>
    {
        public int StudentId { get; set; }
        public int WorkshopId { get; set; }
    }

}
