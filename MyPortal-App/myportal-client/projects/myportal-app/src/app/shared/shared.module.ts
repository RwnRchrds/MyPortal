import { MenuFilterPipe } from './_pipes/menu-filter.pipe';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NameFormatPipe } from './_pipes/name-format.pipe';
import { UserDisplayNamePipe } from './_pipes/user-display-name.pipe';
import { BaseComponentDirective } from '../_directives/base-component/base-component.directive';
import { BaseFormDirective } from '../_directives/base-form/base-form.directive';
import {PortalViewDirective} from '../_directives/portal-view/portal-view.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [MenuFilterPipe, NameFormatPipe, UserDisplayNamePipe],
  exports: [MenuFilterPipe, NameFormatPipe, UserDisplayNamePipe],
  providers: [NameFormatPipe]
})
export class SharedModule { }
