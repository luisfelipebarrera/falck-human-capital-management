export class JwtClaimsConstants {
  private constructor() {}

  public static readonly USER_ID =
    'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier';

  public static readonly USERNAME = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';

  public static readonly ROLE = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
}
