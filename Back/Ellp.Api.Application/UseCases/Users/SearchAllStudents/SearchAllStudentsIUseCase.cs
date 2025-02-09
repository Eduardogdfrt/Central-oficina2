using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using MediatR;

namespace Ellp.Api.Application.UseCases.Users.SearchAllStudents
{
    internal class SearchAllStudentsUseCase : IRequestHandler<SearchAllStudentsInput, IEnumerable<SearchAllStudentsOutput>>
    {
        private readonly IStudentRepository _studentRepository;

        public SearchAllStudentsUseCase(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<SearchAllStudentsOutput>> Handle(SearchAllStudentsInput request, CancellationToken cancellationToken)
        {
            IEnumerable<Student> students;

            if (request.StudentId.HasValue)
            {
                var student = await _studentRepository.GetStudentByIdAsync(request.StudentId.Value);
                students = student != null ? new List<Student> { student } : Enumerable.Empty<Student>();
            }
            else
            {
                students = await _studentRepository.GetAllAsync();
            }

            return students.Select(SearchAllStudentsOutput.FromEntity).ToList();
        }
    }
}

