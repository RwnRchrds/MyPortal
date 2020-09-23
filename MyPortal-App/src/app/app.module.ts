import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import {StaffPortalModule} from './staff-portal/staff-portal.module';
import {StudentPortalModule} from './student-portal/student-portal.module';
import {ParentPortalModule} from './parent-portal/parent-portal.module';
import {CommonModule} from '@angular/common';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
   ],
  imports: [
    CommonModule,
    BrowserModule,
    FlexLayoutModule,
    StaffPortalModule,
    StudentPortalModule,
    ParentPortalModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
