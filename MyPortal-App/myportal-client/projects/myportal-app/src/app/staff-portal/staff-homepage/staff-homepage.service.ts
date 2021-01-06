import { Injectable } from '@angular/core';
import {PortalViewServiceDirective} from '../../shared/portal-view/portal-view-service.directive';
import {AuthService} from '../../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class StaffHomepageService extends PortalViewServiceDirective {

  constructor(authService: AuthService) {
    super(authService);
  }

  reset(): void{
  }
}
