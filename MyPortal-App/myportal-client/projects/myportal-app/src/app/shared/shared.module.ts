import { MenuFilterPipe } from './_pipes/menu-filter.pipe';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NameFormatPipe } from './_pipes/name-format.pipe';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [MenuFilterPipe, NameFormatPipe],
  exports: [MenuFilterPipe, NameFormatPipe]
})
export class SharedModule { }
