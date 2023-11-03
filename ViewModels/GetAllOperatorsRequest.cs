using MediatR;
using System.Collections.Generic;

namespace ViewModels
{
    public class GetAllOperatorsRequest : IRequest<List<GetAllOperatorsResponse>>
    {
    }
}
