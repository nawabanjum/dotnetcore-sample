using Sample.BlazorServer.DTO;

namespace Sample.BlazorServer.Service
{
    public interface IAuthRepository
    {
        public Task<bool> Register(UserDto dto);
        public Task<bool> Login(LoginDTO dto);
        public Task<string> ExternalLogins();
        public Task Logout();
    }
}
