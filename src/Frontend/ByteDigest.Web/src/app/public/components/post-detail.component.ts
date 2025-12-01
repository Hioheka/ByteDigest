import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { PostService } from '../../core/services/post.service';
import { Post } from '../../core/models/post.model';

@Component({
  selector: 'app-post-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="post-container">
      <div *ngIf="loading" class="loading">Yükleniyor...</div>
      
      <article *ngIf="!loading && post" class="post">
        <header class="post-header">
          <h1>{{ post.title }}</h1>
          <div class="post-meta">
            <span class="category">{{ post.category }}</span>
            <span class="date">{{ post.createdAt | date:'dd MMMM yyyy' }}</span>
          </div>
        </header>
        
        <div class="post-content" [innerHTML]="post.content"></div>
        
        <footer class="post-footer">
          <div class="tags" *ngIf="post.tags">
            <span class="tag" *ngFor="let tag of getTags(post.tags)">
              #{{ tag }}
            </span>
          </div>
          <a routerLink="/" class="back-link">← Ana Sayfaya Dön</a>
        </footer>
      </article>
      
      <div *ngIf="!loading && !post" class="not-found">
        <h2>Yazı Bulunamadı</h2>
        <p>Aradığınız yazı bulunamadı veya yayınlanmamış.</p>
        <a routerLink="/" class="back-link">Ana Sayfaya Dön</a>
      </div>
    </div>
  `,
  styles: [`
    .post-container {
      max-width: 800px;
      margin: 0 auto;
      padding: 2rem;
    }
    .loading {
      text-align: center;
      padding: 3rem;
      color: #666;
    }
    .post-header {
      margin-bottom: 2rem;
    }
    .post-header h1 {
      font-size: 2.5rem;
      color: #333;
      margin-bottom: 1rem;
    }
    .post-meta {
      display: flex;
      gap: 1rem;
      align-items: center;
    }
    .category {
      background: #007bff;
      color: white;
      padding: 0.5rem 1rem;
      border-radius: 4px;
      font-size: 0.9rem;
    }
    .date {
      color: #666;
      font-size: 0.9rem;
    }
    .post-content {
      line-height: 1.8;
      color: #333;
      font-size: 1.1rem;
      margin-bottom: 3rem;
    }
    .post-content h2 {
      margin-top: 2rem;
      margin-bottom: 1rem;
    }
    .post-content p {
      margin-bottom: 1rem;
    }
    .post-content code {
      background: #f5f5f5;
      padding: 0.2rem 0.4rem;
      border-radius: 3px;
      font-family: monospace;
    }
    .post-footer {
      border-top: 1px solid #eee;
      padding-top: 2rem;
    }
    .tags {
      display: flex;
      flex-wrap: wrap;
      gap: 0.5rem;
      margin-bottom: 1rem;
    }
    .tag {
      background: #f5f5f5;
      color: #666;
      padding: 0.25rem 0.75rem;
      border-radius: 4px;
      font-size: 0.9rem;
    }
    .back-link {
      color: #007bff;
      text-decoration: none;
      font-weight: 500;
    }
    .back-link:hover {
      text-decoration: underline;
    }
    .not-found {
      text-align: center;
      padding: 3rem;
    }
    .not-found h2 {
      color: #333;
      margin-bottom: 1rem;
    }
    .not-found p {
      color: #666;
      margin-bottom: 2rem;
    }
  `]
})
export class PostDetailComponent implements OnInit {
  post: Post | null = null;
  loading = false;

  constructor(
    private route: ActivatedRoute,
    private postService: PostService
  ) {}

  ngOnInit(): void {
    const slug = this.route.snapshot.paramMap.get('slug');
    if (slug) {
      this.loadPost(slug);
    }
  }

  loadPost(slug: string): void {
    this.loading = true;
    this.postService.getPublishedPostBySlug(slug).subscribe({
      next: (post) => {
        this.post = post;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading post:', error);
        this.loading = false;
      }
    });
  }

  getTags(tags: string): string[] {
    return tags.split(',').map(tag => tag.trim()).filter(tag => tag);
  }
}
