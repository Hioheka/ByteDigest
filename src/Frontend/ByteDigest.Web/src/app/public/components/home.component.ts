import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PostService } from '../../core/services/post.service';
import { Post, PaginatedResult } from '../../core/models/post.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  template: `
    <div class="home-container">
      <header class="header">
        <h1>ByteDigest</h1>
        <p>Yazılım ve Teknoloji İçerikleri</p>
      </header>

      <div class="filters">
        <input 
          type="text" 
          [(ngModel)]="searchTerm"
          (input)="onSearch()"
          placeholder="Yazılarda ara..."
          class="search-input">
        
        <select [(ngModel)]="selectedCategory" (change)="onCategoryChange()" class="category-select">
          <option value="">Tüm Kategoriler</option>
          <option value="Yazılım">Yazılım</option>
          <option value="Teknoloji">Teknoloji</option>
          <option value="Web">Web</option>
        </select>
      </div>

      <div class="posts-grid" *ngIf="!loading && posts.length > 0">
        <article *ngFor="let post of posts" class="post-card">
          <h2>{{ post.title }}</h2>
          <div class="post-meta">
            <span class="category">{{ post.category }}</span>
            <span class="date">{{ post.createdAt | date:'dd.MM.yyyy' }}</span>
          </div>
          <p class="excerpt">{{ post.excerpt }}</p>
          <a [routerLink]="['/yazi', post.slug]" class="read-more">Devamını Oku →</a>
        </article>
      </div>

      <div *ngIf="loading" class="loading">Yükleniyor...</div>
      
      <div *ngIf="!loading && posts.length === 0" class="no-posts">
        Henüz yayınlanmış yazı bulunmamaktadır.
      </div>

      <div class="pagination" *ngIf="paginatedResult && paginatedResult.totalPages > 1">
        <button 
          (click)="previousPage()" 
          [disabled]="!paginatedResult.hasPreviousPage"
          class="btn">
          ← Önceki
        </button>
        <span class="page-info">
          Sayfa {{ paginatedResult.page }} / {{ paginatedResult.totalPages }}
        </span>
        <button 
          (click)="nextPage()" 
          [disabled]="!paginatedResult.hasNextPage"
          class="btn">
          Sonraki →
        </button>
      </div>
    </div>
  `,
  styles: [`
    .home-container {
      max-width: 1200px;
      margin: 0 auto;
      padding: 2rem;
    }
    .header {
      text-align: center;
      margin-bottom: 3rem;
    }
    .header h1 {
      font-size: 3rem;
      color: #333;
      margin-bottom: 0.5rem;
    }
    .header p {
      color: #666;
      font-size: 1.2rem;
    }
    .filters {
      display: flex;
      gap: 1rem;
      margin-bottom: 2rem;
    }
    .search-input, .category-select {
      padding: 0.75rem;
      border: 1px solid #ddd;
      border-radius: 4px;
      font-size: 1rem;
    }
    .search-input {
      flex: 1;
    }
    .posts-grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
      gap: 2rem;
      margin-bottom: 2rem;
    }
    .post-card {
      background: white;
      padding: 1.5rem;
      border-radius: 8px;
      box-shadow: 0 2px 8px rgba(0,0,0,0.1);
    }
    .post-card h2 {
      margin-bottom: 1rem;
      color: #333;
    }
    .post-meta {
      display: flex;
      gap: 1rem;
      margin-bottom: 1rem;
      font-size: 0.9rem;
      color: #666;
    }
    .category {
      background: #007bff;
      color: white;
      padding: 0.25rem 0.75rem;
      border-radius: 4px;
    }
    .excerpt {
      color: #555;
      line-height: 1.6;
      margin-bottom: 1rem;
    }
    .read-more {
      color: #007bff;
      text-decoration: none;
      font-weight: 500;
    }
    .read-more:hover {
      text-decoration: underline;
    }
    .loading, .no-posts {
      text-align: center;
      padding: 3rem;
      color: #666;
    }
    .pagination {
      display: flex;
      justify-content: center;
      align-items: center;
      gap: 1rem;
    }
    .btn {
      padding: 0.5rem 1rem;
      background: #007bff;
      color: white;
      border: none;
      border-radius: 4px;
      cursor: pointer;
    }
    .btn:disabled {
      opacity: 0.5;
      cursor: not-allowed;
    }
    .page-info {
      color: #666;
    }
  `]
})
export class HomeComponent implements OnInit {
  posts: Post[] = [];
  paginatedResult: PaginatedResult<Post> | null = null;
  loading = false;
  currentPage = 1;
  pageSize = 9;
  searchTerm = '';
  selectedCategory = '';

  constructor(private postService: PostService) {}

  ngOnInit(): void {
    this.loadPosts();
  }

  loadPosts(): void {
    this.loading = true;
    this.postService.getPublishedPosts(
      this.currentPage, 
      this.pageSize, 
      this.searchTerm || undefined,
      this.selectedCategory || undefined
    ).subscribe({
      next: (result) => {
        this.paginatedResult = result;
        this.posts = result.items;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading posts:', error);
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.currentPage = 1;
    this.loadPosts();
  }

  onCategoryChange(): void {
    this.currentPage = 1;
    this.loadPosts();
  }

  previousPage(): void {
    if (this.paginatedResult?.hasPreviousPage) {
      this.currentPage--;
      this.loadPosts();
    }
  }

  nextPage(): void {
    if (this.paginatedResult?.hasNextPage) {
      this.currentPage++;
      this.loadPosts();
    }
  }
}
