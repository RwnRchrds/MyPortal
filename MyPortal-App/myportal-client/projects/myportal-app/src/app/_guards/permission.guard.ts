import { AuthService } from '../_services/auth.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import {map, take} from 'rxjs/operators';

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
      return this.authService.effectivePermissions$.pipe(take(1), map((perms: string[]) => {
        if (requiredPermissions.some(p => perms.includes(p.toLowerCase()))) {
          return true;
        }
        this.router.navigate(['login']);
        return false;
      }));
    }
  }
}


