import { AuthService } from './_services/auth.service';
import { TokenModel } from 'myportal-api';
import { Component, OnInit } from '@angular/core';
import {map} from 'rxjs/operators';
import {AppService} from './_services/app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'MyPortal';
  showUi = false;

  constructor(private appService: AppService, private authService: AuthService) {}

ngOnInit(): void {
  this.setCurrentUser();
}

setCurrentUser(): void {
  const tokenWrapper: TokenModel = JSON.parse(localStorage.getItem('token'));

  if (!!tokenWrapper)
  {
    const user = this.authService.getCurrentUser(tokenWrapper);
    this.authService.setCurrentUser(user);
    this.appService.blockPage();
    this.authService.updatePermissions().pipe(map(perm => {
      this.showUi = true;
      // @ts-ignore
      KTLayoutAsideMenu.init('kt_aside_menu');
      this.appService.unblockPage();
    })).subscribe();
  }
  else
  {
    this.authService.setCurrentUser(null);
  }
}

}
