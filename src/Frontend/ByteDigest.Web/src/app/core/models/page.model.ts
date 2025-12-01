export interface Page {
  id: number;
  slug: string;
  title: string;
  content: string;
  isPublished: boolean;
  createdAt: Date;
  updatedAt?: Date;
}

export interface CreatePage {
  slug: string;
  title: string;
  content: string;
  isPublished: boolean;
}

export interface UpdatePage {
  slug: string;
  title: string;
  content: string;
  isPublished: boolean;
}
