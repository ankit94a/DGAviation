import { Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    // canActivate: [AuthGuard],
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        loadComponent: () => import('./dashboard/dashboard.component').then(m => m.DashboardComponent),
      },
      {
        path: 'registration',
        loadComponent: () => import('./user-registration-details/registration-add/registration-add.component').then(m => m.RegistrationAddComponent),
      },
        {
        path: 'candidateList',
        loadComponent: () => import('./user-registration-details/registration-list/registration-list.component').then(m => m.RegistrationListComponent),
      },
      {
      path: 'under-construction',
      loadComponent: () => import('../shared-library/component/under-construction/under-construction.component').then(m => m.UnderConstructionComponent),
    
      }

    ]
  }
]
