import { Injectable } from '@angular/core';
import { User } from './../_models/user';
import {Observable, ReplaySubject} from 'rxjs';
import {map, take} from 'rxjs/operators';
import { AuthenticationService, LoginModel, TokenModel } from 'myportal-api';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  refreshTokenInProgress = false;

  private currentUserSource = new ReplaySubject<User>(1);
  private permissionSource = new ReplaySubject<string[]>(1);

  currentUser$ = this.currentUserSource.asObservable();
  effectivePermissions$ = this.permissionSource.asObservable();

  constructor(private authService: AuthenticationService) {
  }

  login(model: LoginModel): Observable<void> {
    return this.authService.login(model).pipe(
      map((response: TokenModel) => {
        const loginResponse = response;
        const user = this.getUser(loginResponse);
        if (user) {
          localStorage.setItem('token', JSON.stringify(loginResponse));
          this.currentUserSource.next(user);
        }
      }));
  }

  updatePermissions(): Observable<void> {
    return this.authService.getEffectivePermissions().pipe(
      map((response: string[]) => {
      const latestPermissions = response;

      if (latestPermissions) {
        localStorage.setItem('perms', JSON.stringify(latestPermissions));
        this.permissionSource.next(latestPermissions);
      }
    }));
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

  hasPermission(requiredPermissions: string[]): boolean {
    if (!requiredPermissions || requiredPermissions.length === 0) {
      return true;
    }
    else {
      let userPermissions: string[];
      this.effectivePermissions$.pipe(take(1)).subscribe(permissions => userPermissions = permissions);
      for (const perm of requiredPermissions) {
        if (userPermissions.includes(perm.toLowerCase())) {
          return true;
        }
      }
    }
    return false;
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
    this.effectivePermissions$.pipe(take(1)).subscribe(data => console.log(data));
    localStorage.clear();
    this.currentUserSource.next(null);
    this.permissionSource.next(null);
  }

  setCurrentUser(user: User, permissions: string[] = null): void {
    this.currentUserSource.next(user);
    this.permissionSource.next(permissions);
  }
}
