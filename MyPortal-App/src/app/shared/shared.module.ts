import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedComponent } from './shared.component';
import {BrowserModule} from '@angular/platform-browser';
import {FlexLayoutModule} from '@angular/flex-layout';
import { PortalRootDirective } from './portal-root.directive';

@NgModule({
  imports: [
    CommonModule,  
    FlexLayoutModule
  ],
  declarations: [	SharedComponent,
      PortalRootDirective
   ]
})
export class SharedModule { }
