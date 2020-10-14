import { AuthService } from './_services/auth.service';
import { TokenWrapper } from './_models/token-wrapper';
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
  const tokenWrapper: TokenWrapper = JSON.parse(localStorage.getItem('tokenWrapper'));

  if (!!tokenWrapper)
  {
    const user = this.authService.getUser(tokenWrapper);
    this.authService.setCurrentUser(user);
  }
  else
  {
    this.authService.setCurrentUser(null);
  }
}

}
