using Application.ViewModels;
using AutoMapper;
using Bases.Bases;
using Bases.Entities;
using Helpers;
using Localization;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestHandles.Password
{
    public class ResetPasswordService : IRequestHandler<ResetPasswordRequest, Unit>
    {
        private readonly GenericRepository<Operator, int> _operatorRepository;
        private readonly IMapper _mapper;
        private readonly Resources _resources;

        public ResetPasswordService(GenericRepository<Operator, int> operatorRepository, IMapper mapper, Resources resources)
        {
            _operatorRepository = operatorRepository;
            _mapper = mapper;
            _resources = resources;
        }

        public async Task<Unit> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var query = (await _operatorRepository.GetWhere(x => x.Email == request.Email, cancellationToken)).FirstOrDefault();

            if (query == null)
                throw new UnauthorizedAccessException(_resources.InvalidCredentials());

            if (query.AcceptNewPassword == false)
                throw new UnauthorizedAccessException(_resources.InvalidCredentials());

            query.PasswordHash = OperatorHelper.GetPasswordHash(request.Password);
            query.AcceptNewPassword = false;

            await _operatorRepository.Update(query, cancellationToken);

            return Unit.Value;
        }
    }
}
