using AdeptusMart03.BusinessAccessLayer.Services;

namespace AdeptusMart04.Api.Context
{
    public class LoginApiContext
    {
        private readonly LoginService _loginService;

        public LoginApiContext(LoginService loginService)
        {
            _loginService = loginService;
        }
        public async Task<Guid?> LogInAsync(string username, string password)
        {
            try
            {
                return await _loginService.LogIn(username, password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Login işlemi sırasında hata oluştu: {ex.Message}");
            }
        }

    }
}
