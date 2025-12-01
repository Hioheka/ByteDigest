import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PostService } from '../../core/services/post.service';
import { Post } from '../../core/models/post.model';

@Component({
  selector: 'app-admin-posts',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="admin-posts">
      <div class="header">
        <h2>Yazılar</h2>
        <button class="btn-primary" (click)="navigateToCreate()">+ Yeni Yazı</button>
      </div>

      <div *ngIf="loading" class="loading">Yükleniyor...</div>

      <table *ngIf="!loading && posts.length > 0" class="posts-table">
        <thead>
          <tr>
            <th>Başlık</th>
            <th>Kategori</th>
            <th>Durum</th>
            <th>Tarih</th>
            <th>İşlemler</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let post of posts">
            <td>{{ post.title }}</td>
            <td>{{ post.category }}</td>
            <td>
              <span [class]="post.isPublished ? 'status-published' : 'status-draft'">
                {{ post.isPublished ? 'Yayında' : 'Taslak' }}
              </span>
            </td>
            <td>{{ post.createdAt | date:'dd.MM.yyyy' }}</td>
            <td class="actions">
              <button class="btn-edit" (click)="editPost(post.id)">Düzenle</button>
              <button class="btn-delete" (click)="deletePost(post.id)">Sil</button>
            </td>
          </tr>
        </tbody>
      </table>

      <div *ngIf="!loading && posts.length === 0" class="no-data">
        Henüz yazı bulunmamaktadır.
      </div>
    </div>
  `,
  styles: [`
    .admin-posts {
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
    .posts-table {
      width: 100%;
      border-collapse: collapse;
    }
    .posts-table th,
    .posts-table td {
      padding: 1rem;
      text-align: left;
      border-bottom: 1px solid #eee;
    }
    .posts-table th {
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
export class AdminPostsComponent implements OnInit {
  posts: Post[] = [];
  loading = false;

  constructor(private postService: PostService) {}

  ngOnInit(): void {
    this.loadPosts();
  }

  loadPosts(): void {
    this.loading = true;
    this.postService.getAllPosts().subscribe({
      next: (posts) => {
        this.posts = posts;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading posts:', error);
        this.loading = false;
      }
    });
  }

  navigateToCreate(): void {
    alert('Yeni yazı oluşturma formu burada gösterilecek.');
  }

  editPost(id: number): void {
    alert(`Yazı ID ${id} düzenleme formu burada gösterilecek.`);
  }

  deletePost(id: number): void {
    if (confirm('Bu yazıyı silmek istediğinizden emin misiniz?')) {
      this.postService.deletePost(id).subscribe({
        next: () => {
          this.loadPosts();
        },
        error: (error) => {
          console.error('Error deleting post:', error);
          alert('Yazı silinirken bir hata oluştu.');
        }
      });
    }
  }
}
