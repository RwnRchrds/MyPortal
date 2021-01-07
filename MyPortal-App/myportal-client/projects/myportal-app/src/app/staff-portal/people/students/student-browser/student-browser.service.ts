import { Injectable } from '@angular/core';
import {PortalViewServiceDirective} from '../../../../shared/portal-view/portal-view-service.directive';
import {AuthService} from '../../../../_services/auth.service';
import {StudentSearchService} from './student-search/student-search.service';
@Injectable({
  providedIn: 'root'
})
export class StudentBrowserService extends PortalViewServiceDirective {

  constructor(authService: AuthService, private studentSearchService: StudentSearchService) {
    super(authService);
  }

  reset(): void {
    this.studentSearchService.reset();
  }
}
