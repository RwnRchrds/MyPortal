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
import {UserBrowserComponent} from '../settings/users/user-browser/user-browser.component';
import {UserViewComponent} from '../settings/users/user-view/user-view.component';
import {StudentSearchComponent} from '../people/students/student-browser/student-search/student-search.component';
import {RoleSearchComponent} from '../settings/roles/role-browser/role-search/role-search.component';
import {CreateRoleComponent} from '../settings/roles/role-browser/create-role/create-role.component';
import {UserSearchComponent} from '../settings/users/user-browser/user-search/user-search.component';
import {CreateUserComponent} from '../settings/users/create-user/create-user.component';

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
        data: {requiredPermissions: [AppPermissions.STUDENTS_DETAILS_VIEW]},
        children: [
          {
            path: '',
            component: StudentSearchComponent
          }
        ]
      },
      {
        path: 'settings/roles',
        component: RoleBrowserComponent,
        canActivate: [PermissionGuard],
        data: {requiredPermissions: [AppPermissions.SYSTEM_GROUPS_VIEW]},
        children: [
          {
            path: 'new-role',
            component: CreateRoleComponent
          },
          {
            path: '',
            component: RoleSearchComponent
          }
        ]
      },
      {
        path: 'settings/roles/:id',
        component: RoleViewComponent,
        canActivate: [PermissionGuard],
        data: {requiredPermissions: [AppPermissions.SYSTEM_GROUPS_VIEW]}
      },
      {
        path: 'settings/users',
        component: UserBrowserComponent,
        canActivate: [PermissionGuard],
        data: {requiredPermissions: [AppPermissions.SYSTEM_USERS_VIEW]},
        children: [
          {
            path: 'new-user',
            component: CreateUserComponent
          },
          {
            path: '',
            component: UserSearchComponent
          }
        ]
      },
      {
        path: 'settings/users/:id',
        component: UserViewComponent,
        canActivate: [PermissionGuard],
        data: {requiredPermissions: [AppPermissions.SYSTEM_USERS_VIEW]}
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
