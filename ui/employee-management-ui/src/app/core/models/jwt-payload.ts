export interface JwtPayload {
  [key: string]: string | number;

  exp: number;

  iss: string;

  aud: string;
}
