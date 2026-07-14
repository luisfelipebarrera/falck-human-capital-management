import { Component, inject } from '@angular/core';

import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { Router } from '@angular/router';

import { AuthenticationService } from '../../../core/authentication/authentication.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  private readonly fb = inject(FormBuilder);

  private readonly authenticationService = inject(AuthenticationService);

  private readonly router = inject(Router);

  readonly loginForm = this.fb.nonNullable.group({
    username: ['', [Validators.required]],

    password: ['', [Validators.required]],
  });

  isSubmitting = false;

  errorMessage = '';

  login(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.authenticate();
  }

  private authenticate(): void {
    this.isSubmitting = true;

    this.errorMessage = '';

    this.authenticationService

      .login(this.loginForm.getRawValue())

      .subscribe({
        next: () => {
          this.router.navigate(['/employees']);
        },

        error: () => {
          this.errorMessage = 'Invalid username or password.';
        },

        complete: () => {
          this.isSubmitting = false;
        },
      });
  }
}
