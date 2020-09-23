import { StaffHomepageComponent } from './staff-homepage/staff-homepage.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';

import { StaffPortalComponent } from './staff-portal.component';
import { StaffSidebarComponent } from './staff-sidebar/staff-sidebar.component';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule
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
