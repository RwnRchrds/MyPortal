import { Injectable } from '@angular/core';
import {PortalViewServiceDirective} from '../../../../shared/portal-view/portal-view-service.directive';
import {AuthService} from '../../../../_services/auth.service';
@Injectable({
  providedIn: 'root'
})
export class StudentBrowserService extends PortalViewServiceDirective {
  showSearchComponent = true;
  showCreateComponent = false;

  constructor(authService: AuthService) {
    super(authService);
  }

  reset(): void {
    this.showSearchComponent = true;
    this.showCreateComponent = false;
  }
}
