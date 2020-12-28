import { AuthService } from './_services/auth.service';
import { TokenModel } from 'myportal-api';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'MyPortal';

  constructor(private authService: AuthService) {}

ngOnInit(): void {
  this.setCurrentUser();
}

setCurrentUser(): void {
  const tokenWrapper: TokenModel = JSON.parse(localStorage.getItem('token'));
  const permissions: string[] = JSON.parse(localStorage.getItem('perms'));

  if (!!tokenWrapper)
  {
    const user = this.authService.getUser(tokenWrapper);
    this.authService.setCurrentUser(user, permissions);
  }
  else
  {
    this.authService.setCurrentUser(null);
  }
}

}
