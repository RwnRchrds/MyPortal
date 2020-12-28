import { AuthService } from '../_services/auth.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PermissionGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    const requiredPermissions = route.data.requiredPermissions as string[];

    if (!requiredPermissions) {
      return of(true);
    }
    else {
      if (this.authService.hasPermission(requiredPermissions)) {
        return of(true);
      }
      else {
        this.router.navigate(['']);
        return of(false);
      }
    }
  }
}
