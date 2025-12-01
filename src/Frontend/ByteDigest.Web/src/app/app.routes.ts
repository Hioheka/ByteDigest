import { Routes } from '@angular/router';
import { authGuard, adminGuard } from './shared/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./public/public.routes').then(m => m.PUBLIC_ROUTES)
  },
  {
    path: 'giris',
    loadChildren: () => import('./auth/auth.routes').then(m => m.AUTH_ROUTES)
  },
  {
    path: 'admin',
    canActivate: [adminGuard],
    loadChildren: () => import('./admin/admin.routes').then(m => m.ADMIN_ROUTES)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
