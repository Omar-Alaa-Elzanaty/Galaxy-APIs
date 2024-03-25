using Galaxy.Application.Extention;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Galaxy.Application.Features.Customers.Querires.GetAllCustomers
{
    public record GetAllCustomersQuery : PaginatedRequest, IRequest<PaginatedResponse<GetAllCustomersQueryDto>>
    {
        public CustomerColumnName? ColumnName { get; set; }
    }

    internal class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, PaginatedResponse<GetAllCustomersQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCustomersQueryHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<GetAllCustomersQueryDto>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
        {
            var entities = _unitOfWork.Repository<Customer>().Entities();

            if (!query.KeyWord.IsNullOrEmpty())
            {
                entities = entities.Where(x => x.Name.ToLower().Contains(query.KeyWord!.ToLower()));
            }

            if(query.ColumnName is not null)
            {
                switch (query.ColumnName)
                {
                    case CustomerColumnName.Name:
                        entities = entities.OrderBy(x => x.Name);
                        break;

                    case CustomerColumnName.PhoneNumber:
                        entities = entities.OrderBy(x => x.PhoneNumber);
                        break;

                    case CustomerColumnName.CreationDate:
                        entities = entities.OrderBy(x => x.CreationDate);
                        break;
                }
            }

            var customers = await entities.ProjectToType<GetAllCustomersQueryDto>()
                            .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            return customers;
        }
    }
}
