import { StudentRoutingModule } from './student-routing/student-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentPortalComponent } from './student-portal.component';
import {FlexLayoutModule} from '@angular/flex-layout';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    StudentRoutingModule
  ],
  declarations: [StudentPortalComponent]
})
export class StudentPortalModule { }
