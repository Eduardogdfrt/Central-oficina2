using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetStudentWorkshop
{
    public class GetStudentWorkshopOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public WorkshopAluno Data { get; set; }
    }
}













