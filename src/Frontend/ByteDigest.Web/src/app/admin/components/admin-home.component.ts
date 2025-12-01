import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-admin-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="admin-home">
      <h2>Hoş Geldiniz</h2>
      <p>ByteDigest yönetim paneline hoş geldiniz.</p>
      
      <div class="quick-actions">
        <div class="action-card">
          <h3>Yazılar</h3>
          <p>Blog yazılarınızı yönetin</p>
          <a routerLink="/admin/yazilar" class="btn">Yazılara Git</a>
        </div>
        <div class="action-card">
          <h3>Sayfalar</h3>
          <p>Statik sayfalarınızı yönetin</p>
          <a routerLink="/admin/sayfalar" class="btn">Sayfalara Git</a>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .admin-home {
      max-width: 1200px;
      margin: 0 auto;
    }
    h2 {
      font-size: 2rem;
      color: #333;
      margin-bottom: 0.5rem;
    }
    p {
      color: #666;
      margin-bottom: 2rem;
    }
    .quick-actions {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 2rem;
    }
    .action-card {
      background: white;
      padding: 2rem;
      border-radius: 8px;
      box-shadow: 0 2px 8px rgba(0,0,0,0.1);
    }
    .action-card h3 {
      color: #333;
      margin-bottom: 0.5rem;
    }
    .action-card p {
      color: #666;
      margin-bottom: 1rem;
    }
    .btn {
      display: inline-block;
      background: #007bff;
      color: white;
      padding: 0.75rem 1.5rem;
      border-radius: 4px;
      text-decoration: none;
    }
    .btn:hover {
      background: #0056b3;
    }
  `]
})
export class AdminHomeComponent {}
