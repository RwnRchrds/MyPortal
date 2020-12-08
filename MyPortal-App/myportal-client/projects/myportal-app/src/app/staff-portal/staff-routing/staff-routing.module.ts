import { StudentViewComponent } from './../people/students/student-view/student-view.component';
import { StudentBrowserComponent } from './../people/students/student-browser/student-browser.component';
import { StaffAuthGuard } from './../../_guards/staff-auth.guard';
import { StaffPortalComponent } from './../staff-portal.component';
import { StaffHomepageComponent } from './../staff-homepage/staff-homepage.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

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
        component: StudentBrowserComponent
      },
      {
        path: 'students/:id',
        component: StudentViewComponent,
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
