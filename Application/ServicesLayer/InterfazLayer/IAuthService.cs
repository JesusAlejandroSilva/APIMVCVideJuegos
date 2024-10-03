using Domain.EntitiesLayer;
using Microsoft.AspNetCore.Identity;

namespace Application.ServicesLayer.InterfazLayer
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegistroModel model);
        Task<string> LoginUserAsync(LoginModel model);
    }
}
