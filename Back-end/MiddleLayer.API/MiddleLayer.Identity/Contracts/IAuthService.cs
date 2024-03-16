using MiddleLayer.Identity.Models;

namespace MiddleLayer.Identity.Contracts
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);

        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
