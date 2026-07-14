using ApiGateway.Controllers;

namespace ApiGateway.Services;

public sealed class UsersService : IUsersController
{
    private readonly HttpClient _httpClient;

    public UsersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ICollection<UserResponse>> GetUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<UserResponse>>(
                   "/users")
               ?? [];
    }

    public async Task<UserResponse> GetUserAsync(Guid id)
    {
        var response = await _httpClient.GetAsync(
            $"/users/{id}");

        response.EnsureSuccessStatusCode();

        return (await response.Content
            .ReadFromJsonAsync<UserResponse>())!;
    }

    public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "/users",
            request);

        response.EnsureSuccessStatusCode();

        return (await response.Content
            .ReadFromJsonAsync<UserResponse>())!;
    }
}