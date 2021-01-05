import { SharedModule } from '../shared/shared.module';
import { StudentSearchComponent } from './people/students/student-browser/student-search/student-search.component';
import { StudentBrowserComponent } from './people/students/student-browser/student-browser.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { StaffRoutingModule } from './staff-routing/staff-routing.module';
import { StaffHomepageComponent } from './staff-homepage/staff-homepage.component';
import { StaffPortalComponent } from './staff-portal.component';
import { StaffSidebarComponent } from './staff-sidebar/staff-sidebar.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { RoleBrowserComponent } from './settings/roles/role-browser/role-browser.component';
import { RoleSearchComponent } from './settings/roles/role-browser/role-search/role-search.component';
import { RoleViewComponent } from './settings/roles/role-view/role-view.component';
import { RoleDetailsComponent } from './settings/roles/role-view/role-details/role-details.component';
import { CreateRoleComponent } from './settings/roles/role-browser/create-role/create-role.component';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    StaffRoutingModule,
    SharedModule
  ],
  declarations: [
    StaffPortalComponent,
    StaffSidebarComponent,
    StaffHomepageComponent,
    StudentBrowserComponent,
    StudentSearchComponent,
    RoleBrowserComponent,
    RoleSearchComponent,
    RoleViewComponent,
    RoleDetailsComponent,
    CreateRoleComponent
  ],
  exports: [
    StaffPortalComponent,
    StaffSidebarComponent
  ],
  providers: []
})
export class StaffPortalModule { }
