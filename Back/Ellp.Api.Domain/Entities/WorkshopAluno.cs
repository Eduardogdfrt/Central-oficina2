
namespace Ellp.Api.Domain.Entities
{
    public class WorkshopAluno
    {
        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public string Certificate { get; set; }
    }
}