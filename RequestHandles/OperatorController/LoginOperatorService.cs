using AutoMapper;
using Bases.Bases;
using Bases.Entities;
using Helpers;
using Localization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViewModels;

namespace RequestHandles.OperatorController
{
    public class LoginOperatorService : IRequestHandler<LoginOperatorRequest, LoginOperatorResponse>
    {
        private readonly GenericRepository<Operator, int> _operatorRepository;
        private readonly IMapper _mapper;
        private readonly Resources _resources;
        private readonly IConfiguration _configuration;

        public LoginOperatorService(GenericRepository<Operator, int> operatorRepository, IMapper mapper, Resources resources, IConfiguration configuration)
        {
            _operatorRepository = operatorRepository;
            _mapper = mapper;
            _resources = resources;
            _configuration = configuration;
        }

        public async Task<LoginOperatorResponse> Handle(LoginOperatorRequest request, CancellationToken cancellationToken)
        {
            var query = (await _operatorRepository.GetWhere(x => x.Email == request.Email, cancellationToken, q => q.Include(a => a.OperatorRoles))).FirstOrDefault();

            if (query == null)
                throw new UnauthorizedAccessException(_resources.InvalidCredentials());

            var checkPassword = OperatorHelper.VerifyPassword(request.Password, query.PasswordHash);

            if (checkPassword == false)
                throw new UnauthorizedAccessException(_resources.InvalidCredentials());

            var roles = query.OperatorRoles.Select(x => x.RoleId).ToList();

            var token = TokenHelper.GenerateJwtToken(query.Email, _configuration, roles);

            await EmailHelper.SendEmailAsync(query.Email, "Login", $"{query.Name} se logou no dia {DateTime.Now}");

            return _mapper.Map<LoginOperatorResponse>(token);
        }
    }
}
