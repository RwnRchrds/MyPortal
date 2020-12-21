import { SharedModule } from './../shared/shared.module';
import { AppModule } from './../app.module';
import { MenuFilterPipe } from '../shared/_pipes/menu-filter.pipe';
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
import { UserBrowserComponent } from './settings/users/user-browser/user-browser.component';
import { UserSearchComponent } from './settings/users/user-browser/user-search/user-search.component';

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
    UserBrowserComponent,
    UserSearchComponent
  ],
  exports: [
    StaffPortalComponent,
    StaffSidebarComponent
  ],
  providers: []
})
export class StaffPortalModule { }
