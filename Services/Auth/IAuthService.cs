namespace TaskFlow_API.Services.Auth
{
    public interface IAuthService
    {
        public Task<string> Login(LoginDTO dto);
    }
}
