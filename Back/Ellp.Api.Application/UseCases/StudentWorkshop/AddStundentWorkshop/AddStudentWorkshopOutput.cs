using System;

namespace Ellp.Api.Application.UseCases.StudentWorkshop.AddStundentWorkshop
{
    public class AddStudentWorkshopOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static AddStudentWorkshopOutput CreateOutput(bool success, string message)
        {
            return new AddStudentWorkshopOutput
            {
                Success = success,
                Message = message
            };
        }
    }
}





