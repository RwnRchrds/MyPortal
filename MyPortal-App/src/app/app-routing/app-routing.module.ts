import { ParentAuthGuard } from './../_guards/parent-auth.guard';
import { StudentAuthGuard } from './../_guards/student-auth.guard';
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
    path: 'student',
    loadChildren: () => import('../student-portal/student-portal.module').then(m => m.StudentPortalModule),
    canLoad: [StudentAuthGuard]
  },
  {
    path: 'parent',
    loadChildren: () => import('../parent-portal/parent-portal.module').then(m => m.ParentPortalModule),
    canLoad: [ParentAuthGuard]
  },
  {
    path: '**',
    redirectTo: 'login',
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
