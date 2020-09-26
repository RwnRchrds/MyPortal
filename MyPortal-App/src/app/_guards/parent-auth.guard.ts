import { AuthService } from './../_services/auth.service';
import { Injectable } from '@angular/core';
import { CanLoad, Router, CanActivate } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ParentAuthGuard implements CanLoad, CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    const token = this.authService.getCurrentUser();

    if (token.type === '2')
    {
      return true;
    }

    return false;
  }

  canLoad(): boolean {
    const token = this.authService.getCurrentUser();

    if (!!token && token.type === '2')
    {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }

}
