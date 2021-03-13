import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {NewEntityResponse, UsersService} from 'myportal-api';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';
import {BaseFormDirective} from '../../../../../_directives/base-form/base-form.directive';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent extends BaseFormDirective implements OnInit {

  get username(): AbstractControl {
    return this.form.get('username');
  }

  get userType(): AbstractControl {
    return this.form.get('userType');
  }

  get password(): AbstractControl {
    return this.form.get('password');
  }

  get confirmPassword(): AbstractControl {
    return this.form.get('confirmPassword');
  }

  constructor(private userService: UsersService) {
    super();
  }

  ngOnInit(): void {
    this.componentName = 'new_user';
    this.form = new FormGroup({
      username: new FormControl('', [Validators.required]),
      userType: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required])
    });
  }

  goBack(): void {
    this.form.reset();
    this.router.navigate(['/staff/settings/users']);
  }

  submit(): void {
    if (this.validate()) {
      if (this.password.value !== this.confirmPassword.value) {
        this.alertService.error('The passwords you entered do not match.');
        return;
      }
      this.userService.createUser({username: this.username.value, password: this.password.value,
        userType: +this.userType.value, roleIds: []})
        .pipe(map((response: NewEntityResponse) => {
          this.router.navigate([`/staff/settings/users/${response.id}`]);
        }), catchError((err: HttpErrorResponse) => {
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
    }
  }
}
