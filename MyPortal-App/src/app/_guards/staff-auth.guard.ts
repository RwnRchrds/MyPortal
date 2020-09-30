import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AuthService } from './../_services/auth.service';
import { Injectable } from '@angular/core';
import { CanLoad, Router, CanActivate } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class StaffAuthGuard implements CanLoad, CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): Observable<boolean> {
    return this.authService.currentUser$.pipe(map(user => {
      if (!!user)
      {
        if (user.userType === '0')
        {
          return true;
        }
      }

      this.router.navigate(['/login']);
      return false;
    }));
  }

  canLoad(): Observable<boolean> {
    return this.authService.currentUser$.pipe(map(user => {
      if (!!user)
      {
        if (user.userType === '0')
        {
          return true;
        }
      }

      this.router.navigate(['/login']);
      return false;
    }));
  }

}
