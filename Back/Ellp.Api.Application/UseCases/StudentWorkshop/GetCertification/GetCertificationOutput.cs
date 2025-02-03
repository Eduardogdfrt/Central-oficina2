namespace Ellp.Api.Application.UseCases.StudentWorkshop.GetCertification
{
    public class GetCertificationOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Certificate { get; set; }

        public static GetCertificationOutput CreateOutput(bool success, string message, string certificate = null)
        {
            return new GetCertificationOutput
            {
                Success = success,
                Message = message,
                Certificate = certificate
            };
        }
    }
}





