import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';

import { StaffRoutingModule } from './staff-routing/staff-routing.module';

import { StaffHomepageComponent } from './staff-homepage/staff-homepage.component';
import { StaffPortalComponent } from './staff-portal.component';
import { StaffSidebarComponent } from './staff-sidebar/staff-sidebar.component';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    StaffRoutingModule
  ],
  declarations: [
    StaffPortalComponent,
    StaffSidebarComponent,
    StaffHomepageComponent,
  ],
  exports: [
    StaffPortalComponent,
    StaffSidebarComponent
  ],
  providers: []
})
export class StaffPortalModule { }
