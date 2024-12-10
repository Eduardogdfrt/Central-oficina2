using Ellp.Api.Domain.Entities;
using Ellp.Api.Application.Utilities;

namespace Ellp.Api.Application.UseCases.Workshops.AddWorkshops
{
    public static class AddWorkshopMapper
    {
        public static Workshop ToEntity(AddWorkshopInput input)
        {
            return new Workshop
            {
                Name = input.Name,
                Data = input.Data
            };
        }
    }
}
