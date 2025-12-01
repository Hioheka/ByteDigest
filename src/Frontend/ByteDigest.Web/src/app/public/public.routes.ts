import { Routes } from '@angular/router';
import { HomeComponent } from './components/home.component';
import { PostDetailComponent } from './components/post-detail.component';
import { PageDetailComponent } from './components/page-detail.component';

export const PUBLIC_ROUTES: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'yazi/:slug',
    component: PostDetailComponent
  },
  {
    path: 'sayfa/:slug',
    component: PageDetailComponent
  }
];
