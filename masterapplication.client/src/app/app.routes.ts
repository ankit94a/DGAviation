import { Routes } from '@angular/router';
import { AuthGuard } from '../shared-library/guards/auth-guard';
import { AppComponent } from './app.component';
export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'landing'
  },
  {
    path: 'landing',
    loadComponent: () =>
      import('../layout/landing/landing.component').then(m => m.LandingComponent),
  },
  {
    path: '',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('../layout/layout.routes').then(m => m.routes),

  },
  {
    path: '**',
    redirectTo: 'landing'
  }

];
