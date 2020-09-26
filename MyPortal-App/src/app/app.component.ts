import { AuthService } from './_services/auth.service';
import { HttpClient } from '@angular/common/http';
import { User } from './_models/user';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'MyPortal-App';

  constructor(private http: HttpClient, private authService: AuthService) {}

ngOnInit(): void {
  this.setCurrentUser();
}

setCurrentUser(): void {
  const user: User = JSON.parse(localStorage.getItem('user'));
  this.authService.setCurrentUser(user);
}

}