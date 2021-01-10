import { Injectable } from '@angular/core';
import {PortalViewServiceDirective} from '../../../../shared/portal-view/portal-view-service.directive';
import {AuthService} from '../../../../_services/auth.service';
import {AppPermissions} from '../../../../_guards/app-permissions';

@Injectable({
  providedIn: 'root'
})
export class UserBrowserService extends PortalViewServiceDirective {

  showSearchComponent = true;
  showCreateComponent = false;

  get allowViewUsers(): boolean {
    return this.hasPermission([AppPermissions.SYSTEM_USERS_VIEW]);
  }

  get allowEditUsers(): boolean {
    return this.hasPermission([AppPermissions.SYSTEM_USERS_EDIT]);
  }

  showCreate(): void {
    this.showSearchComponent = false;
    this.showCreateComponent = true;
  }

  showSearch(): void {
    this.showCreateComponent = false;
    this.showSearchComponent = true;
  }

  constructor(authService: AuthService) {
    super(authService);
  }

  reset(): void {
    this.showSearch();
  }
}
