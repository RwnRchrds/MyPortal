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

    // If route has no required permissions, allow the user to proceed
    if (!requiredPermissions) {
      return of(true);
    }
    else {
      if (this.authService.hasPermission(requiredPermissions)) {
        // The user has the required permissions - allow them to proceed
        return of(true);
      }
      else {
        // The user does not have the required permissions - redirect to root
        this.router.navigate(['']);
        return of(false);
      }
    }
  }
}


