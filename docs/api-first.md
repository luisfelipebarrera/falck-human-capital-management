# API First

## API Contracts

### Employees

- GET /api/employees - Returns a list of all employees.
- GET /api/employees/{id} - Returns details of a specific employee by ID.
- POST /api/employees - Adds a new employee.
- PUT /api/employees/{id} - Updates an existing employee.
- DELETE /api/employees/{id} - Deletes an employee.

API Gateway implementation

```bash

dotnet tool install -g NSwag.ConsoleCore

# i.e. generate controllers in api-gateway from open-api.yml
# In the NSwag command, use the /jsonLibrary:SystemTextJson parameter to avoid dragging along old third-party dependencies.
nswag openapi2cscontroller /input:"apis/open-api.yml" /output:"src/gateway/api/src/Controllers/ApiEmployeeHcm.cs" /className:"{controller}" /namespace:"ApiGateway.Controllers" /controllerBaseClass:"Microsoft.AspNetCore.Mvc.ControllerBase" /jsonLibrary:"SystemTextJson" /operationGenerationMode:"MultipleClientsFromFirstTagAndOperationId"


```

**Nice to have:** It is possible to have an automation command to regenerate the code every time changes are saved to the open-api.yml file.
