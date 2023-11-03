using Application.ViewModels;
using AutoMapper;
using Bases.Bases;
using Bases.Entities;
using Helpers;
using Localization;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.RequestHandles.Password
{
    public class SendCodeResetPasswordService : IRequestHandler<SendCodeResetPasswordRequest, Unit>
    {
        private readonly GenericRepository<Operator, int> _operatorRepository;
        private readonly IMapper _mapper;
        private readonly Resources _resources;
        private readonly IConfiguration _configuration;

        public SendCodeResetPasswordService(GenericRepository<Operator, int> operatorRepository, IMapper mapper, Resources resources, IConfiguration configuration)
        {
            _operatorRepository = operatorRepository;
            _mapper = mapper;
            _resources = resources;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(SendCodeResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var query = (await _operatorRepository.GetWhere(x => x.Email == request.Email, cancellationToken)).FirstOrDefault();

            if (query != null)
            {
                var code = new Random().Next(100000, 999999);
                var codeHash = OperatorHelper.GetPasswordHash(code.ToString());
                var time = DateTime.UtcNow.AddMinutes(5);

                query.CodeResetPassword = codeHash;
                query.CodeResetPasswordExpiration = time;

                await _operatorRepository.Update(query);

                await EmailHelper.SendEmailAsync(query.Email, "Código de Reset de Senha", $"Código: {code}");
            }

            return Unit.Value;
        }
    }
}
