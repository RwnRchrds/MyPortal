import { Injectable } from '@angular/core';
import { TokenWrapper } from './../_models/token-wrapper';
import { User } from './../_models/user';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import {Observable, ReplaySubject} from 'rxjs';
import {map, take} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  refreshTokenInProgress = false;

  private currentUserSource = new ReplaySubject<User>(1);

  currentUser$ = this.currentUserSource.asObservable();

  constructor(http: HttpClient) {
    super(http, 'auth');
  }

  login(model: any): any {
    return this.http.post(this.baseUrl + 'login', model)
      .pipe(
        map((response: TokenWrapper) => {
          const loginResponse = response;

          const user = this.getUser(loginResponse);

          if (user) {
            console.log(user);
            localStorage.setItem('tokenWrapper', JSON.stringify(loginResponse));
            this.currentUserSource.next(user);
          }
        })
      );
  }

  refreshToken(): Observable<boolean> {
    this.refreshTokenInProgress = true;
    let currentUser: User;
    this.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);
    return this.http.post(this.baseUrl + 'refreshToken', currentUser).pipe(map((tokenResponse: TokenWrapper) => {
      const userResponse = this.getUser(tokenResponse);
      if (userResponse) {
        console.log('refresh success');
        localStorage.setItem('tokenWrapper', JSON.stringify(tokenResponse));
        this.currentUserSource.next(userResponse);
        this.refreshTokenInProgress = false;
        return true;
      }
      this.refreshTokenInProgress = false;
      return false;
    }));
  }

  private getDecodedToken(token: string): any {
    return JSON.parse(atob(token.split('.')[1]));
  }

  getUser(tokenWrapper: TokenWrapper): User {

    const decodedToken = this.getDecodedToken(tokenWrapper.token);

    const user = {
      displayName: decodedToken.displayName,
      userType: decodedToken.type,
      permissions: decodedToken.perm,
      token: tokenWrapper.token,
      refreshToken: tokenWrapper.refreshToken
    } as User;

    return user;
  }

  logout(): void {
    localStorage.removeItem('tokenWrapper');
    this.currentUserSource.next(null);
  }

  setCurrentUser(user: User): void {
    this.currentUserSource.next(user);
  }
}
