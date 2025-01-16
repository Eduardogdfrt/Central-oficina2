using System.Collections.Generic;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Workshops.GetWorkshopAll
{
    public class GetWorkshopAllByprofessorOutput
    {
        public List<Workshop> Workshops { get; set; }

        public GetWorkshopAllByprofessorOutput()
        {
            Workshops = new List<Workshop>();
        }
    }
}











