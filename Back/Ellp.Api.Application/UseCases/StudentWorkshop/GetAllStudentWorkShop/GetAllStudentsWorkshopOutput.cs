using System.Collections.Generic;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetAllStudentWorkshops
{
    public class GetAllStudentWorkshopsOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Workshop> Workshops { get; set; }

        public static GetAllStudentWorkshopsOutput CreateOutput(bool success, string message, List<Workshop> workshops = null)
        {
            return new GetAllStudentWorkshopsOutput
            {
                Success = success,
                Message = message,
                Workshops = workshops
            };
        }
    }
}







