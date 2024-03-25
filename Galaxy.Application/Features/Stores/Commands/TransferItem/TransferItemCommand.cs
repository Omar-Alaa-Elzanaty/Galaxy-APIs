using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Constants;
using Galaxy.Domain.Identity;
using Galaxy.Domain.Models;
using Galaxy.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Stores.Commands.TransferItem
{
    public record TransferItemCommand : IRequest<Response>
    {
        public string UserId { get; set; }
        public bool IsToStore { get; set; }
        public List<string> ItemsBarCode { get; set; } = [];
    }
    internal class TransferItemCommandHandler : IRequestHandler<TransferItemCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<TransferItemCommandHandler> _localization;

        public TransferItemCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<TransferItemCommandHandler> stringLocalizer,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _localization = stringLocalizer;
            _userManager = userManager;
        }

        public async Task<Response> Handle(TransferItemCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);

            if (user is null)
            {
                return await Response.FailureAsync(_localization["UserNotFound"].Value);
            }

            var userRole = _userManager.GetRolesAsync(user).Result.First();

            if ((!command.IsToStore && userRole == Roles.WAREHOUSE) || (command.IsToStore && userRole == Roles.SALE))
            {
                return await Response.FailureAsync(_localization["Unauthorize"].Value);
            }

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
