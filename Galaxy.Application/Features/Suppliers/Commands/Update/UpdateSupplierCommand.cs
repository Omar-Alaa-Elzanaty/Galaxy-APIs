using Galaxy.Application.Features.Suppliers.Commands.Delete;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Pharamcy.Application.Interfaces.Media;

namespace Galaxy.Application.Features.Suppliers.Commands.Update
{
    public record UpdateSupplierCommand : IRequest<Response>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? ProfileImageFile { get; set; }
        public IFormFile? IdImageFile { get; set; }
    }

    internal class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;
        private readonly IStringLocalizer<DeleteSupplierCommandByIdCommand> _localization;

        public UpdateSupplierCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<DeleteSupplierCommandByIdCommand> localization,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
            _mediaService = mediaService;
        }

        public async Task<Response> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.Repository<Supplier>().GetByIdAsync(command.Id);

            if (supplier == null)
            {
                return await Response.FailureAsync(_localization["NoSupplierFound"].Value);
            }

            supplier.Name = command.Name;

            if (command.IdImageFile is not null)
            {
                supplier.IdUrl = await _mediaService.UpdateAsync(supplier.IdUrl, command.IdImageFile);
            }

            if (command.ProfileImageFile is not null)
            {
                supplier.ImageUrl = await _mediaService.UpdateAsync(supplier.ImageUrl, command.ProfileImageFile);
            }

            await _unitOfWork.Repository<Supplier>().UpdateAsync(supplier);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}
