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
import { UserBrowserComponent } from './settings/users/user-browser/user-browser.component';
import { UserSearchComponent } from './settings/users/user-browser/user-search/user-search.component';
import { UserViewComponent } from './settings/users/user-view/user-view.component';
import { UserDetailsComponent } from './settings/users/user-view/user-details/user-details.component';
import { UserLinkPersonComponent } from './settings/users/user-view/user-link-person/user-link-person.component';
import { UserResetPasswordComponent } from './settings/users/user-view/user-reset-password/user-reset-password.component';
import { CreateUserComponent } from './settings/users/create-user/create-user.component';

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
    CreateRoleComponent,
    UserBrowserComponent,
    UserSearchComponent,
    UserViewComponent,
    UserDetailsComponent,
    UserLinkPersonComponent,
    UserResetPasswordComponent,
    CreateUserComponent
  ],
  exports: [
    StaffPortalComponent,
    StaffSidebarComponent
  ],
  providers: []
})
export class StaffPortalModule { }
