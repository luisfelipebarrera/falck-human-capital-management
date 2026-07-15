using ApiGateway.Controllers;

namespace ApiGateway.Services;

public sealed class UsersService : IUsersController
{
    private readonly HttpClient _httpClient;

    public UsersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserPagedResponse> GetUsersAsync(int page, int limit)
    {
        return (await _httpClient.GetFromJsonAsync<UserPagedResponse>(
            $"/users?page={page}&limit={limit}"))!;
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