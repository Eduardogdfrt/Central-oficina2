using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Workshops.GetWorkshopById
{
    public class GetWorkshopByIdOutput
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Workshop Workshop { get; set; }

        public static GetWorkshopByIdOutput ToOutput(Workshop workshop)
        {
            return new GetWorkshopByIdOutput
            {
                Success = true,
                Message = "Workshop found",
                Workshop = workshop
            };
        }

        public static GetWorkshopByIdOutput ToOutputIfNotFound()
        {
            return new GetWorkshopByIdOutput
            {
                Success = false,
                Message = "Workshop not found"
            };
        }
    }
}
