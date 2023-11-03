using Application.ViewModels;
using AutoMapper;
using Bases.Bases;
using Bases.Entities;
using Localization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestHandles.RolesController
{
    public class InsertRoleInOperatorService : IRequestHandler<InsertRoleInOperatorRequest, Unit>
    {
        private readonly GenericRepository<Operator, int> _operatorRepository;
        private readonly IMapper _mapper;
        private readonly Resources _resources;

        public InsertRoleInOperatorService(GenericRepository<Operator, int> operatorRepository, IMapper mapper, Resources resources)
        {
            _operatorRepository = operatorRepository;
            _mapper = mapper;
            _resources = resources;
        }

        public async Task<Unit> Handle(InsertRoleInOperatorRequest request, CancellationToken cancellationToken)
        {
            var query = (await _operatorRepository.GetWhere(x => x.Email == request.Email, cancellationToken, q => q.Include(a => a.OperatorRoles))).FirstOrDefault();
            //var query = (await _operatorRepository.GetWhere(x => x.Email == request.Email, cancellationToken)).FirstOrDefault();

            query.OperatorRoles.Add(new OperatorRole
            {
                RoleId = request.RoleId
            });

            await _operatorRepository.Update(query, cancellationToken);

            return Unit.Value;
        }
    }
}
