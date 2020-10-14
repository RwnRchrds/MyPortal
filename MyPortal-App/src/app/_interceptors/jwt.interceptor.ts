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
import {catchError, take, filter, switchMap, map, finalize} from 'rxjs/operators';
import {Router} from '@angular/router';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(this.injectToken(request)).pipe(
      catchError((error: HttpErrorResponse) => {
      if (request.url.includes('refreshToken')) {
        this.authService.logout();
      }

      if (request.url.includes('login')) {
        return throwError(error);
      }

      if (error.status !== 401) {
        throwError(error);
      }

      if (this.authService.refreshTokenInProgress) {
        console.log('waiting...');
        return this.authService.currentUser$.pipe(
          filter(u => u !== null),
          take(1),
          switchMap(() => next.handle(this.injectToken(request))));
      }
      else {
        this.authService.refreshTokenInProgress = true;
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
