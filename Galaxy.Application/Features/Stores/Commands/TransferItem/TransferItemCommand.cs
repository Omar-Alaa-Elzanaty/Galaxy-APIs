using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Stores.Commands.TransferItem
{
    public record TransferItemCommand : IRequest<Response>
    {
        public bool IsToStore { get; set; }
        public List<string> ItemsBarCode { get; set; } = [];
    }
    internal class TransferItemCommandHandler : IRequestHandler<TransferItemCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<TransferItemCommandHandler> _localization;

        public TransferItemCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<TransferItemCommandHandler> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _localization = stringLocalizer;
        }

        public async Task<Response> Handle(TransferItemCommand command, CancellationToken cancellationToken)
        {
            var items = await _unitOfWork.Repository<Stock>().Entities()
                        .Where(x => command.ItemsBarCode.Contains(x.BarCode))
                        .ToListAsync();

            if (items.Count() == 0)
            {
                return await Response.FailureAsync(_localization["InvalidRequest"].Value);
            }

            foreach (var item in items)
            {
                item.IsInStock = !command.IsToStore;

                await _unitOfWork.Repository<Stock>().UpdateAsync(item);
            }

            _ = await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}
