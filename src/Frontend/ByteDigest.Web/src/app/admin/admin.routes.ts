import { Routes } from '@angular/router';
import { AdminDashboardComponent } from './components/admin-dashboard.component';
import { AdminHomeComponent } from './components/admin-home.component';
import { AdminPostsComponent } from './components/admin-posts.component';
import { AdminPagesComponent } from './components/admin-pages.component';

export const ADMIN_ROUTES: Routes = [
  {
    path: '',
    component: AdminDashboardComponent,
    children: [
      {
        path: '',
        component: AdminHomeComponent
      },
      {
        path: 'yazilar',
        component: AdminPostsComponent
      },
      {
        path: 'sayfalar',
        component: AdminPagesComponent
      }
    ]
  }
];
