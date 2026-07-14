using ApiGateway.Controllers;

namespace ApiGateway.Services;

public sealed class EmployeesService : IEmployeesController
{
    private readonly HttpClient _httpClient;

    public EmployeesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EmployeeResponse> EmployeesGETAsync()
    {
        return (await _httpClient.GetFromJsonAsync<EmployeeResponse>(
            "/api/employees"))!;
    }

    public async Task EmployeesPOSTAsync()
    {
        throw new NotImplementedException();
    }

    public async Task EmployeesGET2Async(
        double id,
        int page,
        int limit)
    {
        throw new NotImplementedException();
    }

    public async Task EmployeesPUTAsync(double id)
    {
        throw new NotImplementedException();
    }

    public async Task EmployeesDELETEAsync(double id)
    {
        throw new NotImplementedException();
    }
}