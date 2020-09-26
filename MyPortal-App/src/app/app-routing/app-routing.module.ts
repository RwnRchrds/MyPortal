import { LoginAuthGuard } from '../_guards/login-auth.guard';
import { StaffAuthGuard } from './../_guards/staff-auth.guard';
import { LoginComponent } from './../login/login.component';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [LoginAuthGuard]
  },
  {
    path: 'staff',
    loadChildren: () => import('../staff-portal/staff-portal.module').then(m => m.StaffPortalModule),
    canLoad: [StaffAuthGuard]
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(
      appRoutes,
      {
        preloadingStrategy: PreloadAllModules
      }
      )
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {

 }
