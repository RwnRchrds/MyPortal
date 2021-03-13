import { AuthService } from './_services/auth.service';
import { TokenModel } from 'myportal-api';
import { Component, OnInit } from '@angular/core';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'MyPortal';
  showUi = false;

  constructor(private authService: AuthService) {}

ngOnInit(): void {
  this.setCurrentUser();
}

setCurrentUser(): void {
  const tokenWrapper: TokenModel = JSON.parse(localStorage.getItem('token'));

  if (!!tokenWrapper)
  {
    const user = this.authService.getCurrentUser(tokenWrapper);
    this.authService.setCurrentUser(user);
    this.authService.updatePermissions().pipe(map(perm => {
      this.showUi = true;
    })).subscribe();
  }
  else
  {
    this.authService.setCurrentUser(null);
  }
}

}
