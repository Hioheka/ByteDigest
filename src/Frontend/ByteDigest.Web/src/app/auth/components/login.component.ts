import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="login-container">
      <div class="login-card">
        <h2>ByteDigest - Giriş</h2>
        <form (ngSubmit)="onSubmit()" #loginForm="ngForm">
          <div class="form-group">
            <label for="userNameOrEmail">Kullanıcı Adı veya E-posta</label>
            <input 
              type="text" 
              id="userNameOrEmail" 
              name="userNameOrEmail"
              [(ngModel)]="credentials.userNameOrEmail"
              required
              class="form-control">
          </div>
          <div class="form-group">
            <label for="password">Şifre</label>
            <input 
              type="password" 
              id="password" 
              name="password"
              [(ngModel)]="credentials.password"
              required
              class="form-control">
          </div>
          <div *ngIf="errorMessage" class="error-message">
            {{ errorMessage }}
          </div>
          <button type="submit" [disabled]="!loginForm.valid || loading" class="btn-primary">
            {{ loading ? 'Giriş yapılıyor...' : 'Giriş Yap' }}
          </button>
        </form>
      </div>
    </div>
  `,
  styles: [`
    .login-container {
      display: flex;
      justify-content: center;
      align-items: center;
      min-height: 100vh;
      background-color: #f5f5f5;
    }
    .login-card {
      background: white;
      padding: 2rem;
      border-radius: 8px;
      box-shadow: 0 2px 10px rgba(0,0,0,0.1);
      width: 100%;
      max-width: 400px;
    }
    h2 {
      text-align: center;
      margin-bottom: 2rem;
      color: #333;
    }
    .form-group {
      margin-bottom: 1.5rem;
    }
    label {
      display: block;
      margin-bottom: 0.5rem;
      color: #555;
      font-weight: 500;
    }
    .form-control {
      width: 100%;
      padding: 0.75rem;
      border: 1px solid #ddd;
      border-radius: 4px;
      font-size: 1rem;
    }
    .form-control:focus {
      outline: none;
      border-color: #007bff;
    }
    .btn-primary {
      width: 100%;
      padding: 0.75rem;
      background-color: #007bff;
      color: white;
      border: none;
      border-radius: 4px;
      font-size: 1rem;
      cursor: pointer;
    }
    .btn-primary:hover:not(:disabled) {
      background-color: #0056b3;
    }
    .btn-primary:disabled {
      opacity: 0.6;
      cursor: not-allowed;
    }
    .error-message {
      color: #dc3545;
      margin-bottom: 1rem;
      padding: 0.75rem;
      background-color: #f8d7da;
      border-radius: 4px;
    }
  `]
})
export class LoginComponent {
  credentials = {
    userNameOrEmail: '',
    password: ''
  };
  loading = false;
  errorMessage = '';
  returnUrl = '/';

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  onSubmit(): void {
    this.loading = true;
    this.errorMessage = '';

    this.authService.login(this.credentials.userNameOrEmail, this.credentials.password)
      .subscribe({
        next: (response) => {
          this.loading = false;
          if (response.role === 'Admin') {
            this.router.navigate(['/admin']);
          } else {
            this.router.navigate([this.returnUrl]);
          }
        },
        error: (error) => {
          this.loading = false;
          this.errorMessage = 'Giriş başarısız. Lütfen bilgilerinizi kontrol edin.';
        }
      });
  }
}
