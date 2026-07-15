using ApiGateway.Controllers;

namespace ApiGateway.Services;

public sealed class EmployeesService : IEmployeesController
{
    private readonly HttpClient _httpClient;

    public EmployeesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task CreateEmployeeAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteEmployeeAsync(double id)
    {
        throw new NotImplementedException();
    }

    public Task GetEmployeeAsync(double id)
    {
        throw new NotImplementedException();
    }

    public async Task<EmployeeResponse> GetEmployeesAsync(int page, int limit)
    {
        return (await _httpClient.GetFromJsonAsync<EmployeeResponse>(
            "/api/employees"))!;
    }

    public Task UpdateEmployeeAsync(double id)
    {
        throw new NotImplementedException();
    }
}