import { AuthService } from '../_services/auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginAuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {

    const token = this.authService.getCurrentUser();

    if (!!token)
    {
      const userType = token.type;

      if (userType === '0')
      {
        this.router.navigate(['/staff/home']);
        return false;
      }
      else if (userType === '1')
      {
        this.router.navigate(['/student/home']);
        return false;
      }
      else if (userType === '2')
      {
        this.router.navigate(['/parent/home']);
        return false;
      }
    }

    return true;
}
}
