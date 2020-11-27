import { User } from '../_models/user';
import { AuthService } from '../_services/auth.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import {catchError, take, filter, switchMap} from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(this.injectToken(request)).pipe(
      catchError((error: HttpErrorResponse) => {

      // Logout user if token refresh fails
      if (request.url.includes('refreshToken')) {
        this.authService.logout();
      }

      // No need to attempt token refresh if login fails
      if (request.url.includes('login')) {
        return throwError(error);
      }

      // No need to attempt token refresh if error is not 401: Unauthorised
      if (error.status !== 401) {
        return throwError(error);
      }

      // If token refresh already in progress, wait for completion and get the result
      if (this.authService.refreshTokenInProgress) {
        return this.authService.currentUser$.pipe(
          filter(u => u !== null),
          take(1),
          switchMap(() => next.handle(this.injectToken(request))));
      }

      // Refresh token from
      else {
        return this.authService.refreshToken().pipe(switchMap(success => {
          return next.handle(this.injectToken(request));
        }));
      }
    })
    );
  }

  injectToken(request: HttpRequest<unknown>): HttpRequest<unknown> {
    let currentUser: User;
    this.authService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);
    if (currentUser) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`
        }
      });
    }

    return request;
  }
}
