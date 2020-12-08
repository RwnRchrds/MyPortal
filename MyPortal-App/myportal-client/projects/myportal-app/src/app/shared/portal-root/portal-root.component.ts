import { Router } from '@angular/router';
import { AuthService } from './../../_services/auth.service';
import { Renderer2 } from '@angular/core';

export abstract class PortalRootComponent {

  constructor(protected renderer: Renderer2, public authService: AuthService, protected router: Router) { }

  addBodyClasses(): void {
    this.renderer.addClass(document.body, 'header-fixed');
    this.renderer.addClass(document.body, 'header-mobile-fixed');
    this.renderer.addClass(document.body, 'subheader-enabled');
    this.renderer.addClass(document.body, 'subheader-fixed');
    this.renderer.addClass(document.body, 'aside-enabled');
    this.renderer.addClass(document.body, 'aside-fixed');
    this.renderer.addClass(document.body, 'aside-minimize-hoverable');
    // this.renderer.addClass(document.body, 'aside-minimize');
  }

  removeBodyClasses(): void {
    this.renderer.removeClass(document.body, 'header-fixed');
    this.renderer.removeClass(document.body, 'header-mobile-fixed');
    this.renderer.removeClass(document.body, 'subheader-enabled');
    this.renderer.removeClass(document.body, 'subheader-fixed');
    this.renderer.removeClass(document.body, 'aside-enabled');
    this.renderer.removeClass(document.body, 'aside-fixed');
    this.renderer.removeClass(document.body, 'aside-minimize-hoverable');
    // this.renderer.removeClass(document.body, 'aside-minimize');
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
