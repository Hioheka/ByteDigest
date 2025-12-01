import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Post, CreatePost, UpdatePost, PaginatedResult } from '../models/post.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiUrl = `${environment.apiUrl}/posts`;
  private adminApiUrl = `${environment.apiUrl}/admin/posts`;

  constructor(private http: HttpClient) { }

  getPublishedPosts(page: number = 1, pageSize: number = 10, search?: string, category?: string): Observable<PaginatedResult<Post>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    if (search) {
      params = params.set('search', search);
    }

    if (category) {
      params = params.set('category', category);
    }

    return this.http.get<PaginatedResult<Post>>(this.apiUrl, { params });
  }

  getPublishedPostBySlug(slug: string): Observable<Post> {
    return this.http.get<Post>(`${this.apiUrl}/${slug}`);
  }

  getAllPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(this.adminApiUrl);
  }

  getPostById(id: number): Observable<Post> {
    return this.http.get<Post>(`${this.adminApiUrl}/${id}`);
  }

  createPost(post: CreatePost): Observable<Post> {
    return this.http.post<Post>(this.adminApiUrl, post);
  }

  updatePost(id: number, post: UpdatePost): Observable<Post> {
    return this.http.put<Post>(`${this.adminApiUrl}/${id}`, post);
  }

  deletePost(id: number): Observable<void> {
    return this.http.delete<void>(`${this.adminApiUrl}/${id}`);
  }
}
