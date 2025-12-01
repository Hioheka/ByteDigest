import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PageService } from '../../core/services/page.service';
import { Page } from '../../core/models/page.model';

@Component({
  selector: 'app-admin-pages',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="admin-pages">
      <div class="header">
        <h2>Sayfalar</h2>
        <button class="btn-primary" (click)="navigateToCreate()">+ Yeni Sayfa</button>
      </div>

      <div *ngIf="loading" class="loading">Yükleniyor...</div>

      <table *ngIf="!loading && pages.length > 0" class="pages-table">
        <thead>
          <tr>
            <th>Başlık</th>
            <th>Slug</th>
            <th>Durum</th>
            <th>Tarih</th>
            <th>İşlemler</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let page of pages">
            <td>{{ page.title }}</td>
            <td>{{ page.slug }}</td>
            <td>
              <span [class]="page.isPublished ? 'status-published' : 'status-draft'">
                {{ page.isPublished ? 'Yayında' : 'Taslak' }}
              </span>
            </td>
            <td>{{ page.createdAt | date:'dd.MM.yyyy' }}</td>
            <td class="actions">
              <button class="btn-edit" (click)="editPage(page.id)">Düzenle</button>
              <button class="btn-delete" (click)="deletePage(page.id)">Sil</button>
            </td>
          </tr>
        </tbody>
      </table>

      <div *ngIf="!loading && pages.length === 0" class="no-data">
        Henüz sayfa bulunmamaktadır.
      </div>
    </div>
  `,
  styles: [`
    .admin-pages {
      background: white;
      padding: 2rem;
      border-radius: 8px;
    }
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 2rem;
    }
    h2 {
      margin: 0;
      color: #333;
    }
    .btn-primary {
      background: #28a745;
      color: white;
      border: none;
      padding: 0.75rem 1.5rem;
      border-radius: 4px;
      cursor: pointer;
      font-size: 1rem;
    }
    .btn-primary:hover {
      background: #218838;
    }
    .loading, .no-data {
      text-align: center;
      padding: 2rem;
      color: #666;
    }
    .pages-table {
      width: 100%;
      border-collapse: collapse;
    }
    .pages-table th,
    .pages-table td {
      padding: 1rem;
      text-align: left;
      border-bottom: 1px solid #eee;
    }
    .pages-table th {
      background: #f8f9fa;
      font-weight: 600;
      color: #333;
    }
    .status-published {
      background: #28a745;
      color: white;
      padding: 0.25rem 0.75rem;
      border-radius: 4px;
      font-size: 0.85rem;
    }
    .status-draft {
      background: #6c757d;
      color: white;
      padding: 0.25rem 0.75rem;
      border-radius: 4px;
      font-size: 0.85rem;
    }
    .actions {
      display: flex;
      gap: 0.5rem;
    }
    .btn-edit,
    .btn-delete {
      padding: 0.5rem 1rem;
      border: none;
      border-radius: 4px;
      cursor: pointer;
      font-size: 0.9rem;
    }
    .btn-edit {
      background: #007bff;
      color: white;
    }
    .btn-edit:hover {
      background: #0056b3;
    }
    .btn-delete {
      background: #dc3545;
      color: white;
    }
    .btn-delete:hover {
      background: #c82333;
    }
  `]
})
export class AdminPagesComponent implements OnInit {
  pages: Page[] = [];
  loading = false;

  constructor(private pageService: PageService) {}

  ngOnInit(): void {
    this.loadPages();
  }

  loadPages(): void {
    this.loading = true;
    this.pageService.getAllPages().subscribe({
      next: (pages) => {
        this.pages = pages;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading pages:', error);
        this.loading = false;
      }
    });
  }

  navigateToCreate(): void {
    alert('Yeni sayfa oluşturma formu burada gösterilecek.');
  }

  editPage(id: number): void {
    alert(`Sayfa ID ${id} düzenleme formu burada gösterilecek.`);
  }

  deletePage(id: number): void {
    if (confirm('Bu sayfayı silmek istediğinizden emin misiniz?')) {
      this.pageService.deletePage(id).subscribe({
        next: () => {
          this.loadPages();
        },
        error: (error) => {
          console.error('Error deleting page:', error);
          alert('Sayfa silinirken bir hata oluştu.');
        }
      });
    }
  }
}
