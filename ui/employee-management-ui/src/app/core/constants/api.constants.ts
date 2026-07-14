import { environment } from '../../environments/environment';

export class ApiConstants {
  private constructor() {}

  public static readonly AUTH = `${environment.apiGatewayUrl}/auth`;
  public static readonly EMPLOYEES = `${environment.apiGatewayUrl}/api/employees`;
}
