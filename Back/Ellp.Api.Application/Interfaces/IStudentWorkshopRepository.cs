using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellp.Api.Application.Interfaces
{
    public interface IStudentWorkshopRepository
    {
        Task AddStudentWorkshopAsync(int studentId, int workshopId);
    }
}
