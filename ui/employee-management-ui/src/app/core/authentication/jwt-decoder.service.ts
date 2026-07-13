import { Injectable } from '@angular/core';

import { jwtDecode } from 'jwt-decode';

import { JwtPayload } from '../models/jwt-payload';

@Injectable({
  providedIn: 'root',
})
export class JwtDecoderService {
  decode(token: string): JwtPayload {
    return jwtDecode<JwtPayload>(token);
  }
}
