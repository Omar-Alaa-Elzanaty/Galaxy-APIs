using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Shared;
using MediatR;
using Microsoft.Extensions.Localization;
using Pharamcy.Application.Interfaces.Media;

namespace Galaxy.Application.Features.Products.Commands.Delete
{
    public record DeleteProductCommand : IRequest<Response>
    {
        public int Id { get; set; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediaService _mediaService;
        private readonly IStringLocalizer<DeleteProductCommandHandler> _localization;

        public DeleteProductCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<DeleteProductCommandHandler> localization,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
            _mediaService = mediaService;
        }

        public async Task<Response> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Domain.Models.Product>().GetByIdAsync(command.Id);

            if (entity is null)
            {
                return await Response.FailureAsync(_localization["ItemNotFound"].Value);
            }

            await _mediaService.DeleteAsync(entity.ImageUrl);

            await _unitOfWork.Repository<Domain.Models.Product>().DeleteAsync(entity);
            _ = await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}
