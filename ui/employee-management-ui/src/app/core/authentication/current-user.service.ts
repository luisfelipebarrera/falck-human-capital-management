import { Injectable, Signal, computed, inject, signal } from '@angular/core';

import { User } from '../models/user';

import { JwtClaimsConstants } from '../constants/jwt-claims.constants';

import { JwtDecoderService } from './jwt-decoder.service';

import { TokenStorageService } from './token-storage.service';

@Injectable({
  providedIn: 'root',
})
export class CurrentUserService {
  private readonly tokenStorage = inject(TokenStorageService);

  private readonly jwtDecoder = inject(JwtDecoderService);

  private readonly userSignal = signal<User | null>(null);

  readonly currentUser = this.userSignal.asReadonly();

  constructor() {
    this.loadCurrentUser();
  }

  refresh(): void {
    this.loadCurrentUser();
  }

  clear(): void {
    this.userSignal.set(null);
  }

  private loadCurrentUser(): void {
    const token = this.tokenStorage.get();

    if (!token) {
      this.userSignal.set(null);

      return;
    }

    const payload = this.jwtDecoder.decode(token);

    this.userSignal.set({
      id: payload[JwtClaimsConstants.USER_ID] as string,

      username: payload[JwtClaimsConstants.USERNAME] as string,

      role: payload[JwtClaimsConstants.ROLE] as string,
    });
  }
}
