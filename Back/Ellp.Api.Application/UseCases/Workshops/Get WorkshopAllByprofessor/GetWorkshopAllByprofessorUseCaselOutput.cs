using System.Collections.Generic;
using Ellp.Api.Domain.Entities;

namespace Ellp.Api.Application.UseCases.Workshops.GetWorkshopAll
{
    public class GetWorkshopAllByprofessorUseCaselOutput
    {
        public List<Workshop> Workshops { get; set; }

        public GetWorkshopAllByprofessorUseCaselOutput()
        {
            Workshops = new List<Workshop>();
        }
    }
}











