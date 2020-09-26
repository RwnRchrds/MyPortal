import { AuthService } from './../_services/auth.service';
import { PortalRootComponent } from './../shared/portal-root/portal-root.component';
import {Component, OnDestroy, OnInit, Renderer2} from '@angular/core';

@Component({
  selector: 'app-staff-portal',
  templateUrl: './staff-portal.component.html',
  styleUrls: ['./staff-portal.component.css']
})
export class StaffPortalComponent extends PortalRootComponent implements OnInit, OnDestroy {

  displayName = '';

  constructor(renderer: Renderer2, private authService: AuthService) {
    super(renderer);
  }

  ngOnInit(): void {
    super.addBodyClasses();
    this.loadCurrentUser();
  }

  ngOnDestroy(): void {
    super.removeBodyClasses();
  }

  loadCurrentUser(): void {
    const token = this.authService.getCurrentUser();
    this.displayName = token.displayName;
  }
}
