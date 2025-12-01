export interface Post {
  id: number;
  slug: string;
  title: string;
  excerpt: string;
  content: string;
  category: string;
  tags: string;
  isPublished: boolean;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreatePost {
  slug: string;
  title: string;
  excerpt: string;
  content: string;
  category: string;
  tags: string;
  isPublished: boolean;
}

export interface UpdatePost {
  slug: string;
  title: string;
  excerpt: string;
  content: string;
  category: string;
  tags: string;
  isPublished: boolean;
}

export interface PaginatedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
