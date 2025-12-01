import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { PageService } from '../../core/services/page.service';
import { Page } from '../../core/models/page.model';

@Component({
  selector: 'app-page-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="page-container">
      <div *ngIf="loading" class="loading">Yükleniyor...</div>
      
      <article *ngIf="!loading && page" class="page">
        <header class="page-header">
          <h1>{{ page.title }}</h1>
        </header>
        
        <div class="page-content" [innerHTML]="page.content"></div>
        
        <footer class="page-footer">
          <a routerLink="/" class="back-link">← Ana Sayfaya Dön</a>
        </footer>
      </article>
      
      <div *ngIf="!loading && !page" class="not-found">
        <h2>Sayfa Bulunamadı</h2>
        <p>Aradığınız sayfa bulunamadı veya yayınlanmamış.</p>
        <a routerLink="/" class="back-link">Ana Sayfaya Dön</a>
      </div>
    </div>
  `,
  styles: [`
    .page-container {
      max-width: 800px;
      margin: 0 auto;
      padding: 2rem;
    }
    .loading {
      text-align: center;
      padding: 3rem;
      color: #666;
    }
    .page-header {
      margin-bottom: 2rem;
    }
    .page-header h1 {
      font-size: 2.5rem;
      color: #333;
    }
    .page-content {
      line-height: 1.8;
      color: #333;
      font-size: 1.1rem;
      margin-bottom: 3rem;
    }
    .page-content h2 {
      margin-top: 2rem;
      margin-bottom: 1rem;
    }
    .page-content p {
      margin-bottom: 1rem;
    }
    .page-footer {
      border-top: 1px solid #eee;
      padding-top: 2rem;
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
export class PageDetailComponent implements OnInit {
  page: Page | null = null;
  loading = false;

  constructor(
    private route: ActivatedRoute,
    private pageService: PageService
  ) {}

  ngOnInit(): void {
    const slug = this.route.snapshot.paramMap.get('slug');
    if (slug) {
      this.loadPage(slug);
    }
  }

  loadPage(slug: string): void {
    this.loading = true;
    this.pageService.getPublishedPageBySlug(slug).subscribe({
      next: (page) => {
        this.page = page;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading page:', error);
        this.loading = false;
      }
    });
  }
}
