import { Component, OnInit } from '@angular/core';
import {UserBrowserService} from '../user-browser/user-browser.service';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {AlertService} from '../../../../_services/alert.service';
import {NewEntityResponse, UsersService} from 'myportal-api';
import {catchError, map} from 'rxjs/operators';
import {Router} from '@angular/router';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {

  viewService: UserBrowserService;

  newUserForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    userType: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required])
  });

  get username(): AbstractControl {
    return this.newUserForm.get('username');
  }

  get userType(): AbstractControl {
    return this.newUserForm.get('userType');
  }

  get password(): AbstractControl {
    return this.newUserForm.get('password');
  }

  get confirmPassword(): AbstractControl {
    return this.newUserForm.get('confirmPassword');
  }

  constructor(userBrowserService: UserBrowserService, private alertService: AlertService, private userService: UsersService,
              private router: Router) {
    this.viewService = userBrowserService;
  }

  ngOnInit(): void {
  }

  goBack(): void {
    this.newUserForm.reset();
    this.viewService.showSearch();
  }

  save(): void {
    this.newUserForm.markAllAsTouched();
    if (this.newUserForm.invalid) {
      this.alertService.error('Please review the errors and try again.');
      return;
    }
    if (this.password.value !== this.confirmPassword.value) {
      this.alertService.error('The passwords you entered do not match.');
      return;
    }
    this.userService.createUser({username: this.username.value, password: this.password.value, userType: +this.userType.value, roleIds: []})
      .pipe(map((response: NewEntityResponse) => {
        this.router.navigate([`/staff/settings/users/${response.id}`]);
      }), catchError((err: HttpErrorResponse) => {
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
  }
}
