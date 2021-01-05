import { StudentViewComponent } from './../people/students/student-view/student-view.component';
import { StudentBrowserComponent } from './../people/students/student-browser/student-browser.component';
import { StaffAuthGuard } from './../../_guards/staff-auth.guard';
import { PermissionGuard } from '../../_guards/permission.guard';
import { StaffPortalComponent } from './../staff-portal.component';
import { StaffHomepageComponent } from './../staff-homepage/staff-homepage.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AppPermissions } from '../../_guards/app-permissions';
import {RoleBrowserComponent} from '../settings/roles/role-browser/role-browser.component';
import {RoleViewComponent} from '../settings/roles/role-view/role-view.component';

const staffRoutes: Routes = [
  {
    path: 'staff',
    component: StaffPortalComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [StaffAuthGuard],
    children: [
      {
        path: 'home',
        component: StaffHomepageComponent
      },
      {
        path: 'students',
        component: StudentBrowserComponent,
        canActivate: [PermissionGuard],
        data: {requiredPermissions: [AppPermissions.students_studentDetails_view]}
      },
      {
        path: 'students/:id',
        component: StudentViewComponent,
        canActivate: [PermissionGuard],
        data: {requiredPermissions: [AppPermissions.students_studentDetails_view]}
      },
      {
        path: 'settings/roles',
        component: RoleBrowserComponent,
        canActivate: [PermissionGuard],
        data: {requiredPermissions: [AppPermissions.system_users_view]}
      },
      {
        path: 'settings/roles/:id',
        component: RoleViewComponent
      },
      {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(staffRoutes)
  ],
  exports: [RouterModule]
})
export class StaffRoutingModule { }
