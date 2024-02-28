using Galaxy.Domain.Identity;

namespace Pharamcy.Application.Interfaces.Auth
{
    public interface IAuthServices
    {
        string GenerateToken(ApplicationUser user, string role);
    }
}
