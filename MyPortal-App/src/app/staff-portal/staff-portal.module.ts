import { StudentSearchComponent } from './people/students/student-browser/student-search/student-search.component';
import { StudentBrowserComponent } from './people/students/student-browser/student-browser.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';

import { StaffRoutingModule } from './staff-routing/staff-routing.module';

import { StaffHomepageComponent } from './staff-homepage/staff-homepage.component';
import { StaffPortalComponent } from './staff-portal.component';
import { StaffSidebarComponent } from './staff-sidebar/staff-sidebar.component';
import {FormsModule} from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    FormsModule,
    StaffRoutingModule
  ],
  declarations: [
    StaffPortalComponent,
    StaffSidebarComponent,
    StaffHomepageComponent,
    StudentBrowserComponent,
    StudentSearchComponent
  ],
  exports: [
    StaffPortalComponent,
    StaffSidebarComponent
  ],
  providers: []
})
export class StaffPortalModule { }
