using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetStudentWorkshop
{
    public class GetStudentWorkshopOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public WorkshopAluno Data { get; set; }

        public static GetStudentWorkshopOutput CreateOutput(bool success, string message, WorkshopAluno data = null)
        {
            return new GetStudentWorkshopOutput
            {
                Success = success,
                Message = message,
                Data = data
            };
        }
    }
}




