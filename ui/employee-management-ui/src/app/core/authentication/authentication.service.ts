import { Injectable, inject, Signal } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { tap } from 'rxjs/operators';

import { ApiConstants } from '../constants/api.constants';

import { LoginRequest } from '../models/login-request';

import { LoginResponse } from '../models/login-response';

import { User } from '../models/user';

import { TokenStorageService } from './token-storage.service';

import { CurrentUserService } from './current-user.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private readonly http = inject(HttpClient);

  private readonly tokenStorage = inject(TokenStorageService);
  
  private readonly currentUserService = inject(CurrentUserService);

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${ApiConstants.AUTH}/login`, request).pipe(
      tap((response) => {
        this.tokenStorage.save(response.token);

        this.currentUserService.refresh();
      }),
    );
  }

  logout(): void {
    this.tokenStorage.clear();

    this.currentUserService.clear();
  }

  isAuthenticated(): boolean {
    return this.tokenStorage.hasToken();
  }

  currentUser(): Signal<User | null> {
    return this.currentUserService.currentUser;
  }
}
