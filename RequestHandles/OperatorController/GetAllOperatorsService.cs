using AutoMapper;
using Bases.Bases;
using Bases.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ViewModels;

namespace RequestHandles.OperatorController
{
    public class GetAllOperatorsService : IRequestHandler<GetAllOperatorsRequest, List<GetAllOperatorsResponse>>
    {
        private readonly GenericRepository<Operator, int> _operatorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllOperatorsService> _logger;

        public GetAllOperatorsService(GenericRepository<Operator, int> operatorRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<GetAllOperatorsService> logger)
        {
            _operatorRepository = operatorRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<List<GetAllOperatorsResponse>> Handle(GetAllOperatorsRequest request, CancellationToken cancellationToken)
        {
            var traceIdentifier = _httpContextAccessor.HttpContext.Items["TraceIdentifier"] as string;
            _logger.LogInformation($"TraceIdentifier: {traceIdentifier}");

            var query = _operatorRepository.Queryable(c => c.Id > 0);

            var operators = await query.ToListAsync(cancellationToken);

            var getAllOperatorsResponse = _mapper.Map<List<GetAllOperatorsResponse>>(operators);

            return getAllOperatorsResponse;
        }
    }
}
