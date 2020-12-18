import { AuthService } from '../_services/auth.service';
import { ScriptService } from '../_services/script.service';
import { Component, OnInit } from '@angular/core';
import {LoginModel, SchoolsService} from 'myportal-api';
import {AppService} from '../_services/app.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginModel: LoginModel;

  loginError = '';

  schoolName = '';
  schoolMotto = 'Leap Into Learning';

  constructor(private scriptService: ScriptService, private appService: AppService,
              private authService: AuthService, private schoolService: SchoolsService) {
  }

  ngOnInit(): void {
    this.scriptService.loadStyleSheet('../../assets/lib/css/pages/login/login-2.css');
    this.schoolService.getLocalSchoolName().subscribe(next => {
      this.schoolName = next;
    });
  }

  login(): void {
    this.appService.blockPage();
    this.authService.login(this.loginModel).subscribe(next => {
      this.appService.unblockPage();
      location.reload();
    }, error => {
      console.log(error);
      this.loginError = error.error;
      this.appService.unblockPage();
    });
  }

}
