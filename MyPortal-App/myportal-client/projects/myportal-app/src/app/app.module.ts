import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CommonModule } from '@angular/common';

import { StaffPortalModule } from './staff-portal/staff-portal.module';
import { StudentPortalModule } from './student-portal/student-portal.module';
import { ParentPortalModule } from './parent-portal/parent-portal.module';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { ApiModule, BASE_PATH } from 'myportal-api';
import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
   ],
  imports: [
    CommonModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    FlexLayoutModule,
    StaffPortalModule,
    StudentPortalModule,
    ParentPortalModule,
    AppRoutingModule,
    ApiModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: BASE_PATH, useValue: environment.apiUrl}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
