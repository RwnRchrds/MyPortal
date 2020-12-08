import { map } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginAuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): Observable<boolean> {
    return this.authService.currentUser$.pipe(map(user => {
      if (!!user)
      {
        if (user.userType === '0')
        {
          this.router.navigate(['/staff/home']);
          return false;
        }
        else if (user.userType === '1')
        {
          this.router.navigate(['/student/home']);
          return false;
        }
        else if (user.userType === '2')
        {
          this.router.navigate(['/parent/home']);
          return false;
        }
      }

      return true;
    }));
}
}
