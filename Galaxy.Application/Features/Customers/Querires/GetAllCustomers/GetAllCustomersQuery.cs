using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;

namespace Galaxy.Application.Features.Customers.Querires.GetAllCustomers
{
    public record GetAllCustomersQuery:IRequest<Response>;

    internal class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.Repository<Customer>().GetAllAsync();

            var customers = _mapper.Map<List<GetAllCustomersQueryDto>>(entities);

            return await Response.SuccessAsync(customers);
        }
    }
}
