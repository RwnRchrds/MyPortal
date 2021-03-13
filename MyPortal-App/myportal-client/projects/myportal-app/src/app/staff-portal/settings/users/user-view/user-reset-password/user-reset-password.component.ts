import {Component, OnDestroy, OnInit} from '@angular/core';
import {UserViewService} from '../user-view.service';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {UserModel, UsersService} from 'myportal-api';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';
import {BaseFormDirective} from '../../../../../_directives/base-form/base-form.directive';

@Component({
  selector: 'app-user-reset-password',
  templateUrl: './user-reset-password.component.html',
  styleUrls: ['./user-reset-password.component.css']
})
export class UserResetPasswordComponent extends BaseFormDirective implements OnInit, OnDestroy {

  get password(): AbstractControl {
    return this.form.get('password');
  }

  get confirmPassword(): AbstractControl {
    return this.form.get('confirmPassword');
  }

  constructor(private viewService: UserViewService, private userService: UsersService) {
    super();
  }

  submit(): void {
    if (this.validate()) {
      if (this.password.value !== this.confirmPassword.value) {
        this.alertService.error('The passwords you entered do not match.');
        return;
      }
      const sub = this.viewService.currentUser.pipe(map((user: UserModel) => {
        this.userService.setPassword({userId: user.id, newPassword: this.password.value}).pipe(map((result: string) => {
          this.alertService.toastSuccess('Password reset');
          this.goBack();
        }), catchError((err: HttpErrorResponse) => {
          console.log('Error detected');
          console.log(err);
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      })).subscribe();
      sub.unsubscribe();
    }
  }

  goBack(): void {
    this.form.reset();
    this.viewService.showDetails();
  }

  ngOnInit(): void {
    this.componentName = 'user_reset_password';
    this.form = new FormGroup({
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required])
    });
  }

  ngOnDestroy(): void {
    this.form.reset();
  }
}
