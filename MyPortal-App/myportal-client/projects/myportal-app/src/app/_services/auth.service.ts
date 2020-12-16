import { Injectable } from '@angular/core';
import { User } from './../_models/user';
import {Observable, ReplaySubject} from 'rxjs';
import {map, take} from 'rxjs/operators';
import { AuthenticationService, TokenModel } from 'myportal-api';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  refreshTokenInProgress = false;

  private currentUserSource = new ReplaySubject<User>(1);

  currentUser$ = this.currentUserSource.asObservable();

  constructor(private authService: AuthenticationService) {
  }

  login(model: any): any {
    return this.authService.login(model).pipe(
      map((response: TokenModel) => {
        const loginResponse = response;

        const user = this.getUser(loginResponse);

        if (user) {
          console.log(user);
          localStorage.setItem('token', JSON.stringify(loginResponse));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  refreshToken(): Observable<boolean> {
    this.refreshTokenInProgress = true;
    let currentUser: User;
    this.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);
    return this.authService.refreshToken(currentUser).pipe(map((tokenResponse: TokenModel) => {
      const userResponse = this.getUser(tokenResponse);
      if (userResponse) {
        console.log('refresh success');
        localStorage.setItem('token', JSON.stringify(tokenResponse));
        this.currentUserSource.next(userResponse);
        this.refreshTokenInProgress = false;
        return true;
      }
      this.refreshTokenInProgress = false;
      return false;
    }));
  }

  private getDecodedToken(token: string): any {
    if (!!token)
    {
      return JSON.parse(atob(token.split('.')[1]));
    }
  }

  getUser(tokenModel: TokenModel): User {

    const decodedToken = this.getDecodedToken(tokenModel.token);

    const user = {
      displayName: decodedToken.displayName,
      userType: decodedToken.type,
      permissions: decodedToken.perm,
      token: tokenModel.token,
      refreshToken: tokenModel.refreshToken
    } as User;

    return user;
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }

  setCurrentUser(user: User): void {
    this.currentUserSource.next(user);
  }
}
