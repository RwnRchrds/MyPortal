import { ParentAuthGuard } from './../../_guards/parent-auth.guard';
import { ParentPortalComponent } from './../parent-portal.component';
import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

const parentRoutes: Routes = [
  {
    path: 'parent',
    component: ParentPortalComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [ParentAuthGuard],
    children: [

    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(parentRoutes)
  ],
  exports: [RouterModule]
})
export class ParentRoutingModule { }
