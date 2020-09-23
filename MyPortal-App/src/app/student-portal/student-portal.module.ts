import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentPortalComponent } from './student-portal.component';
import {FlexLayoutModule} from '@angular/flex-layout';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule
  ],
  declarations: [StudentPortalComponent]
})
export class StudentPortalModule { }
