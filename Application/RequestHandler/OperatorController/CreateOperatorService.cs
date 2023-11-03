using Application.ViewModels;
using AutoMapper;
using Bases.Bases;
using Bases.Entities;
using Exceptions;
using Helpers;
using Localization;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestHandles.OperatorController
{
    public class CreateOperatorService : IRequestHandler<CreateOperatorViewModel, Unit>
    {
        private readonly GenericRepository<Operator, int> _operatorRepository;
        private readonly IMapper _mapper;
        private readonly Resources _resources;

        public CreateOperatorService(GenericRepository<Operator, int> operatorRepository, IMapper mapper, Resources resources)
        {
            _operatorRepository = operatorRepository;
            _mapper = mapper;
            _resources = resources;
        }

        public async Task<Unit> Handle(CreateOperatorViewModel request, CancellationToken cancellationToken)
        {
            var result = await _operatorRepository.GetWhere(x => x.Email == request.Email);

            if (result.Count > 0)
                throw new ConflictException(_resources.EmailAlreadyExists());

            request.Password = OperatorHelper.GetPasswordHash(request.Password);
            var newOperator = _mapper.Map<Operator>(request);
            await _operatorRepository.Insert(newOperator);

            return Unit.Value;
        }
    }
}
