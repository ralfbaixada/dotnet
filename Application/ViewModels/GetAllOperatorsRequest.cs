using MediatR;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class GetAllOperatorsRequest : IRequest<List<GetAllOperatorsResponse>>
    {
    }
}
