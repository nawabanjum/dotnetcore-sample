using Sample.BlazorWebAssembly.DTO;

namespace Sample.BlazorWebAssembly.Service
{
    public interface IAuthRepository
    {
        public Task<bool> Register(UserDto dto);
        public Task<bool> Login(LoginDTO dto);
        public Task Logout();
    }
}
