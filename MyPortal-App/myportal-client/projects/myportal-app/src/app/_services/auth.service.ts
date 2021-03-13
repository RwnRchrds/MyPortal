import {Injectable} from '@angular/core';
import {User} from './../_models/user';
import {Observable, ReplaySubject, throwError} from 'rxjs';
import {catchError, map, take} from 'rxjs/operators';
import {AuthenticationService, LoginModel, TokenModel} from 'myportal-api';
import {HttpErrorResponse} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private authService: AuthenticationService) {
  }

  refreshTokenInProgress = false;

  private currentUserSource = new ReplaySubject<User>(1);
  private permissionSource = new ReplaySubject<string[]>(1);

  currentUser$ = this.currentUserSource.asObservable();
  effectivePermissions$ = this.permissionSource.asObservable();

  private static getDecodedToken(token: string): any {
    if (!!token)
    {
      return JSON.parse(atob(token.split('.')[1]));
    }
  }

  login(model: LoginModel): Observable<void> {
    return this.authService.login(model).pipe(
      map((response: TokenModel) => {
        const loginResponse = response;
        const user = this.getCurrentUser(loginResponse);
        if (user) {
          localStorage.setItem('token', JSON.stringify(loginResponse));
          this.currentUserSource.next(user);
        }
      }), catchError((err: HttpErrorResponse) => {
        console.log(err);
        return throwError(err);
      }));
  }

  updatePermissions(): Observable<void> {
    console.log('Updating permissions...');
    return this.authService.getEffectivePermissions().pipe(
      map((response: string[]) => {
      const latestPermissions = response;
      if (latestPermissions) {
        this.permissionSource.next(latestPermissions);
        console.log('Permissions updated.');
      }
    }));
  }

  refreshToken(): Observable<boolean> {
    this.refreshTokenInProgress = true;
    let currentUser: User;
    this.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);
    return this.authService.refreshToken(currentUser).pipe(map((tokenResponse: TokenModel) => {
      const userResponse = this.getCurrentUser(tokenResponse);
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

  getCurrentUser(tokenModel: TokenModel): User {
    const decodedToken = AuthService.getDecodedToken(tokenModel.token);
    return {
      displayName: decodedToken.displayName,
      userType: decodedToken.type,
      token: tokenModel.token,
      refreshToken: tokenModel.refreshToken
    } as User;
  }

  logout(): void {
    localStorage.clear();
    this.currentUserSource.next(null);
    this.permissionSource.next(null);
  }

  setCurrentUser(user: User): void {
    this.currentUserSource.next(user);
  }
}
