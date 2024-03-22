using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;
using Microsoft.Extensions.Localization;
using Pharamcy.Application.Interfaces.Media;

namespace Galaxy.Application.Features.Suppliers.Commands.Delete
{
    public record DeleteSupplierCommandByIdCommand : IRequest<Response>
    {
        public int Id { get; set; }

        public DeleteSupplierCommandByIdCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeleteSupplierCommandByIdCommandHandler : IRequestHandler<DeleteSupplierCommandByIdCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;
        private readonly IStringLocalizer<DeleteSupplierCommandByIdCommand> _localization;
        public DeleteSupplierCommandByIdCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<DeleteSupplierCommandByIdCommand> localization,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
            _mediaService = mediaService;
        }

        public async Task<Response> Handle(DeleteSupplierCommandByIdCommand command, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Repository<Supplier>().GetByIdAsync(command.Id);

            if(supplier is null)
            {
                return await Response.FailureAsync(_localization["NoSupplierFound"].Value);
            }

            await _mediaService.DeleteAsync(supplier.IdUrl);
            await _mediaService.DeleteAsync(supplier.ImageUrl);

            await _unitOfWork.Repository<Supplier>().DeleteAsync(supplier);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}
