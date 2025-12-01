import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="admin-container">
      <nav class="admin-nav">
        <h1>ByteDigest Yönetim Paneli</h1>
        <div class="nav-links">
          <a routerLink="/admin" routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}">Dashboard</a>
          <a routerLink="/admin/yazilar" routerLinkActive="active">Yazılar</a>
          <a routerLink="/admin/sayfalar" routerLinkActive="active">Sayfalar</a>
          <button (click)="logout()" class="logout-btn">Çıkış Yap</button>
        </div>
      </nav>
      <main class="admin-content">
        <router-outlet></router-outlet>
      </main>
    </div>
  `,
  styles: [`
    .admin-container {
      min-height: 100vh;
      background: #f5f5f5;
    }
    .admin-nav {
      background: #333;
      color: white;
      padding: 1rem 2rem;
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
    .admin-nav h1 {
      font-size: 1.5rem;
      margin: 0;
    }
    .nav-links {
      display: flex;
      gap: 1rem;
      align-items: center;
    }
    .nav-links a {
      color: white;
      text-decoration: none;
      padding: 0.5rem 1rem;
      border-radius: 4px;
    }
    .nav-links a:hover,
    .nav-links a.active {
      background: #555;
    }
    .logout-btn {
      background: #dc3545;
      color: white;
      border: none;
      padding: 0.5rem 1rem;
      border-radius: 4px;
      cursor: pointer;
    }
    .logout-btn:hover {
      background: #c82333;
    }
    .admin-content {
      padding: 2rem;
    }
  `]
})
export class AdminDashboardComponent {
  constructor(private authService: AuthService) {}

  logout(): void {
    this.authService.logout();
    window.location.href = '/';
  }
}
