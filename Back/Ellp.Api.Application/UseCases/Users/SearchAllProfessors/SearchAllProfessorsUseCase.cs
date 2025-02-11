using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ellp.Api.Application.Interfaces;
using Ellp.Api.Domain.Entities;
using MediatR;

namespace Ellp.Api.Application.UseCases.Users.SearchAllProfessors
{
    internal class SearchAllProfessorsUseCase : IRequestHandler<SearchAllProfessorsInput, IEnumerable<SearchAllProfessorsOutput>>
    {
        private readonly IProfessorRepository _professorRepository;

        public SearchAllProfessorsUseCase(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<IEnumerable<SearchAllProfessorsOutput>> Handle(SearchAllProfessorsInput request, CancellationToken cancellationToken)
        {
            IEnumerable<Professor> professors;

            if (request.ProfessorId.HasValue)
            {
                var professor = await _professorRepository.GetProfessorByIdAsync(request.ProfessorId.Value);
                professors = professor != null ? new List<Professor> { professor } : Enumerable.Empty<Professor>();
            }
            else
            {
                professors = await _professorRepository.GetAllAsync();
            }

            return professors.Select(SearchAllProfessorsOutput.FromEntity).ToList();
        }
    }
}
