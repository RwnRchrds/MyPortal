import { StudentViewComponent } from './../people/students/student-view/student-view.component';
import { StudentBrowserComponent } from './../people/students/student-browser/student-browser.component';
import { StaffAuthGuard } from './../../_guards/staff-auth.guard';
import { PermissionGuard } from '../../_guards/permission.guard';
import { StaffPortalComponent } from './../staff-portal.component';
import { StaffHomepageComponent } from './../staff-homepage/staff-homepage.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AppPermissions } from '../../_guards/app-permissions';

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
