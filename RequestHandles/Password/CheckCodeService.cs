using Bases.Bases;
using Bases.Entities;
using Helpers;
using Localization;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViewModels;

namespace RequestHandles.Password
{
    public class CheckCodeService : IRequestHandler<CheckCodeRequest, Unit>
    {
        private readonly GenericRepository<Operator, int> _operatorRepository;
        private readonly Resources _resources;
        public CheckCodeService(GenericRepository<Operator, int> operatorRepository, Resources resources)
        {
            _operatorRepository = operatorRepository;
            _resources = resources;
        }
        public async Task<Unit> Handle(CheckCodeRequest request, CancellationToken cancellationToken)
        {
            var query = (await _operatorRepository.GetWhere(x => x.Email == request.Email, cancellationToken)).FirstOrDefault();

            if (query == null)
                throw new UnauthorizedAccessException(_resources.InvalidCredentials());

            var result = OperatorHelper.VerifyPassword(request.Code, query.CodeResetPassword);

            if (result == false)
                throw new UnauthorizedAccessException(_resources.InvalidCode());

            query.AcceptNewPassword = true;
            await _operatorRepository.Update(query, cancellationToken);

            return Unit.Value;
        }
    }
}
