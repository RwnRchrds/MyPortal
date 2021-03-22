import { MenuFilterPipe } from './_pipes/menu-filter.pipe';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NameFormatPipe } from './_pipes/name-format.pipe';
import { UserDisplayNamePipe } from './_pipes/user-display-name.pipe';
import { PostalAddressPipe } from './_pipes/postal-address.pipe';
import {NgInitDirective} from '../_directives/ng-init/ng-init.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [MenuFilterPipe, NameFormatPipe, UserDisplayNamePipe, PostalAddressPipe, NgInitDirective],
    exports: [MenuFilterPipe, NameFormatPipe, UserDisplayNamePipe, PostalAddressPipe, NgInitDirective],
  providers: [NameFormatPipe]
})
export class SharedModule { }
