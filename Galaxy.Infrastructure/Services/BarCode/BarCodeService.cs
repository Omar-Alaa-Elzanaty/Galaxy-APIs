using Galaxy.Application.Interfaces.BarCode;
using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Models;
using Galaxy.Shared.ErrorHandling.Exceptions;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Galaxy.Infrastructure.Services.BarCode
{
    public class BarCodeService : IBarCodeSerivce
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<BarCodeService> _localization;


        public BarCodeService(
            IUnitOfWork unitOfWork,
            IStringLocalizer<BarCodeService> localization)
        {
            _unitOfWork = unitOfWork;
            _localization = localization;
        }

        public async Task<string> GenerateItemCode(int productId,string yearCode,int quantity)
        {
            await Task.CompletedTask;

            throw new BadRequestException(_localization["GenerateBarCodeError"].Value);
        }
        public string CompleteString(string code, int totalLength)
        {
            return new string('0', totalLength - code.Length) + code;
        }
    }
}
