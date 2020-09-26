import { User } from './../_models/user';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'https://localhost:44313/api/auth/';

  private currentUserSource = new ReplaySubject<User>(1);

  currentUser$ = this.currentUserSource.asObservable();

constructor(private http: HttpClient) { }

login(model: any): any {
  return this.http.post(this.baseUrl + 'login', model)
    .pipe(
      map((response: User) => {
        const user = response;

        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
}

logout(): void {
  localStorage.removeItem('user');
  this.currentUserSource.next(null);
}

setCurrentUser(user: User): void {
  this.currentUserSource.next(user);
}

getCurrentUser(): any {
  const user = JSON.parse(localStorage.getItem('user'));

  if (!!user)
  {
    const base64Url = user.token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(c => {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
  }

  return null;
}

loggedIn(): boolean {
  const token = localStorage.getItem('token');
  return !!token;
}

}
