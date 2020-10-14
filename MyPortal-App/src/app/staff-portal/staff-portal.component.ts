import { Router } from '@angular/router';
import { AuthService } from './../_services/auth.service';
import { PortalRootComponent } from './../shared/portal-root/portal-root.component';
import {Component, OnDestroy, OnInit, Renderer2} from '@angular/core';

@Component({
  selector: 'app-staff-portal',
  templateUrl: './staff-portal.component.html',
  styleUrls: ['./staff-portal.component.css']
})
export class StaffPortalComponent extends PortalRootComponent implements OnInit, OnDestroy {

  constructor(renderer: Renderer2, authService: AuthService, router: Router) {
    super(renderer, authService, router);
  }

  ngOnInit(): void {
    super.addBodyClasses();
  }

  ngOnDestroy(): void {
    super.removeBodyClasses();
  }
}
