using System.Collections.Generic;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentsForWorkshop
{
    public class GetAllStudentsForWorkshopOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Student> Students { get; set; }

        public static GetAllStudentsForWorkshopOutput CreateOutput(bool success, string message, List<Student> students = null)
        {
            return new GetAllStudentsForWorkshopOutput
            {
                Success = success,
                Message = message,
                Students = students
            };
        }
    }
}








