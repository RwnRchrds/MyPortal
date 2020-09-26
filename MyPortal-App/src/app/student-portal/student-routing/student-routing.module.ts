import { StudentAuthGuard } from './../../_guards/student-auth.guard';
import { StudentPortalComponent } from './../student-portal.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

const studentRoutes: Routes = [
  {
    path: 'student',
    component: StudentPortalComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [StudentAuthGuard],
    children: [

    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(studentRoutes)
  ],
  exports: [RouterModule]
})
export class StudentRoutingModule { }
