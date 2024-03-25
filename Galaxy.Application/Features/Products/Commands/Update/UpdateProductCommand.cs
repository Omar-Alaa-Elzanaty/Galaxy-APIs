using FluentValidation;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Pharamcy.Application.Interfaces.Media;

namespace Galaxy.Application.Features.Products.Commands.Update
{
    public record UpdateProductCommand : IRequest<Response>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int Rating { get; set; }
        public int LowInventoryIn { get; set; }
    }

    internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateProductCommand> _validator;
        private readonly IStringLocalizer<UpdateProductCommandHandler> _localization;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;

        public UpdateProductCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<UpdateProductCommand> validator,
            IStringLocalizer<UpdateProductCommandHandler> localization,
            IMapper mapper,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _localization = localization;
            _mapper = mapper;
            _mediaService = mediaService;
        }

        public async Task<Response> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            var entity = await _unitOfWork.Repository<Domain.Models.Product>().GetByIdAsync(command.Id);

            if (entity is null)
            {
                return await Response.FailureAsync(_localization["ItemNotFound"].Value);
            }

            _mapper.Map(entity,command);
            entity.ImageUrl = await _mediaService.UpdateAsync(entity.ImageUrl, command.ImageFile);

            await _unitOfWork.Repository<Domain.Models.Product>().UpdateAsync(entity);

            return await Response.SuccessAsync(entity.Id, _localization["Success"].Value);
        }
    }
}
