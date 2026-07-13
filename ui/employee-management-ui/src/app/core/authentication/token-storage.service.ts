import { Injectable } from '@angular/core';

import { StorageConstants } from '../constants/storage.constants';

@Injectable({
  providedIn: 'root',
})
export class TokenStorageService {
  save(token: string): void {
    localStorage.setItem(StorageConstants.ACCESS_TOKEN, token);
  }

  get(): string | null {
    return localStorage.getItem(StorageConstants.ACCESS_TOKEN);
  }

  clear(): void {
    localStorage.removeItem(StorageConstants.ACCESS_TOKEN);
  }

  hasToken(): boolean {
    return this.get() !== null;
  }
}
