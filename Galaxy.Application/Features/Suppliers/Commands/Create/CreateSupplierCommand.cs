using FluentValidation;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Pharamcy.Application.Interfaces.Media;

namespace Galaxy.Application.Features.Suppliers.Commands.Create
{
    public record CreateSupplierCommand : IRequest<Response>
    {
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public IFormFile IdFile { get; set; }
    }
    internal class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateSupplierCommand> _validator;
        private readonly IStringLocalizer<CreateSupplierCommandHandler> _localization;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;

        public CreateSupplierCommandHandler(
            IUnitOfWork unitOfWork,
            IValidator<CreateSupplierCommand> validator,
            IStringLocalizer<CreateSupplierCommandHandler> localization,
            IMapper mapper,
            IMediaService mediaService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _localization = localization;
            _mapper = mapper;
            _mediaService = mediaService;
        }

        public async Task<Response> Handle(CreateSupplierCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(_localization["InvalidReqeust"].Value);
            }

            if (await _unitOfWork.Repository<Supplier>().Entities().AnyAsync(x => x.Name.ToLower().Contains(command.Name.ToLower())))
            {
                return await Response.FailureAsync(_localization["SupplierExist"].Value);
            }

            var supplier = _mapper.Map<Supplier>(command);
            supplier.ImageUrl = await _mediaService.SaveAsync(command.ImageFile);
            supplier.IdUrl = await _mediaService.SaveAsync(command.IdFile);

            await _unitOfWork.Repository<Supplier>().AddAsync(supplier);
            _ = await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(supplier.Id, _localization["Success"].Value);
        }
    }
}
