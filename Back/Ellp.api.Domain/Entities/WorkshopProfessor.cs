using System;
using System.Collections.Generic;

namespace Ellp.Api.Domain.Entities
{
    public class WorkshopProfessor
    {
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
    }
}
