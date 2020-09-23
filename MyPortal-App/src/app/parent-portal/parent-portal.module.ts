import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ParentPortalComponent } from './parent-portal.component';
import {FlexLayoutModule} from '@angular/flex-layout';

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule
  ],
  declarations: [ParentPortalComponent]
})
export class ParentPortalModule { }
