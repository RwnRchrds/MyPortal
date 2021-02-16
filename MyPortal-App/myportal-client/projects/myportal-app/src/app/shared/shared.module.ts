import { MenuFilterPipe } from './_pipes/menu-filter.pipe';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NameFormatPipe } from './_pipes/name-format.pipe';
import { UserDisplayNamePipe } from './_pipes/user-display-name.pipe';
import { BaseComponentDirective } from './base-component.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [MenuFilterPipe, NameFormatPipe, UserDisplayNamePipe, BaseComponentDirective],
  exports: [MenuFilterPipe, NameFormatPipe, UserDisplayNamePipe],
  providers: [NameFormatPipe]
})
export class SharedModule { }
