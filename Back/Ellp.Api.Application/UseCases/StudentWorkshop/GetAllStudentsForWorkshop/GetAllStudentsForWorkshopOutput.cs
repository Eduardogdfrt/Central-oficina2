using System.Collections.Generic;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentsForWorkshop
{
    public class GetAllStudentsForWorkshopOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Student> Students { get; set; }
    }
}
















