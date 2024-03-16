using FluentValidation;
using Galaxy.Application.Interfaces.BarCode;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Pharamcy.Application.Interfaces.Media;

namespace Galaxy.Application.Features.Products.Commands.Create
{
    public record AddProductCommand : IRequest<Response>
    {
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public int Rating { get; set; }
        public int MinimumInventoryIn { get; set; }
    }
    internal class AddProductCommandHandler : IRequestHandler<AddProductCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddProductCommand> _validator;
        private readonly IStringLocalizer<AddProductCommandHandler> _localization;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;
        private readonly IBarCodeSerivce _barCodeSerivce;

        public AddProductCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<AddProductCommand> validator,
            IStringLocalizer<AddProductCommandHandler> localization,
            IMapper mapper,
            IMediaService mediaService,
            IBarCodeSerivce barCodeSerivce)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _localization = localization;
            _mapper = mapper;
            _mediaService = mediaService;
            _barCodeSerivce = barCodeSerivce;
        }

        public async Task<Response> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            if (await _unitOfWork.Repository<Product>().Entities().AnyAsync(x => x.Name == command.Name))
            {
                return await Response.FailureAsync(_localization["ProductExist"].Value);
            }

            var product = _mapper.Map<Product>(command);
            product.ImageUrl = await _mediaService.SaveAsync(command.ImageFile);


            var productCount = await _unitOfWork.Repository<Product>().Entities().CountAsync(cancellationToken: cancellationToken) + 1;
            product.SerialCode = _barCodeSerivce.CompleteString(productCount.ToString(), 4);

            await _unitOfWork.Repository<Product>().AddAsync(product);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(product.Id);
        }
    }
}
