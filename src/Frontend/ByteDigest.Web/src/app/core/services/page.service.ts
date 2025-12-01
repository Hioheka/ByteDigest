import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Page, CreatePage, UpdatePage } from '../models/page.model';

@Injectable({
  providedIn: 'root'
})
export class PageService {
  private apiUrl = `${environment.apiUrl}/pages`;
  private adminApiUrl = `${environment.apiUrl}/admin/pages`;

  constructor(private http: HttpClient) { }

  getPublishedPageBySlug(slug: string): Observable<Page> {
    return this.http.get<Page>(`${this.apiUrl}/${slug}`);
  }

  getAllPages(): Observable<Page[]> {
    return this.http.get<Page[]>(this.adminApiUrl);
  }

  getPageById(id: number): Observable<Page> {
    return this.http.get<Page>(`${this.adminApiUrl}/${id}`);
  }

  createPage(page: CreatePage): Observable<Page> {
    return this.http.post<Page>(this.adminApiUrl, page);
  }

  updatePage(id: number, page: UpdatePage): Observable<Page> {
    return this.http.put<Page>(`${this.adminApiUrl}/${id}`, page);
  }

  deletePage(id: number): Observable<void> {
    return this.http.delete<void>(`${this.adminApiUrl}/${id}`);
  }
}
