import { AuthService } from '../_services/auth.service';
import { ScriptService } from '../_services/script.service';
import { Component, OnInit } from '@angular/core';
import {LoginModel, SchoolsService} from 'myportal-api';
import {AppService} from '../_services/app.service';
import {catchError, flatMap, map, switchMap} from 'rxjs/operators';
import {AbstractControl, FormControl, FormGroup} from '@angular/forms';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl('')
  });

  loginError = '';

  schoolName = '';
  schoolMotto = 'Leap Into Learning';

  constructor(private scriptService: ScriptService, private appService: AppService,
              private authService: AuthService, private schoolService: SchoolsService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.scriptService.loadStyleSheet('../../assets/lib/css/pages/login/login-2.css');
    this.schoolService.getLocalSchoolName().subscribe(next => {
      this.schoolName = next;
    });
  }

  get username(): AbstractControl {
    return this.loginForm.get('username');
  }

  get password(): AbstractControl {
    return this.loginForm.get('password');
  }

  login(): void {
    this.appService.blockPage();
    this.authService.login({username: this.username.value, password: this.password.value}).pipe(map(result => {
      this.authService.updatePermissions().subscribe(perms => {
        this.appService.unblockPage();
        location.reload();
      });
    }), catchError((err: HttpErrorResponse) => {
      this.appService.unblockPage();
      this.loginError = err.error;
      return throwError(err);
    })).subscribe();
  }

}
