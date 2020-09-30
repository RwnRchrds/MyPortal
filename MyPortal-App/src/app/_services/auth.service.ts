import { BaseService } from './base.service';
import { User } from './../_models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  private currentUserSource = new ReplaySubject<User>(1);

  currentUser$ = this.currentUserSource.asObservable();

constructor(http: HttpClient) {
  super(http, 'auth');
 }

login(model: any): any {
  return this.http.post(this.baseUrl + 'login', model)
    .pipe(
      map((response: User) => {
        const user = response;

        const token = this.getDecodedToken(user.token);

        user.displayName = token.displayName;
        user.userType = token.type;

        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
}

getDecodedToken(token: string): any {
  return JSON.parse(atob(token.split('.')[1]));
}

logout(): void {
  localStorage.removeItem('user');
  this.currentUserSource.next(null);
}

setCurrentUser(user: User): void {
  this.currentUserSource.next(user);
}

}
