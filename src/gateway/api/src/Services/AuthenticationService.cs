using ApiGateway.Controllers;
using ApiGateway.Exceptions;

namespace ApiGateway.Services
{
    public class AuthenticationService : IAuthenticationController
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest body)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", body);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedGatewayException("Incorrect credentials.");
            }

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result ?? new LoginResponse();
        }

    }
}