using System.Collections.Generic;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Workshops.GetWorkshopAll
{
    public class GetWorkshopAllOutput
    {
        public List<Workshop> Workshops { get; set; }

        public GetWorkshopAllOutput()
        {
            Workshops = new List<Workshop>();
        }
    }
}











