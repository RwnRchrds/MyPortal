import {Component, OnDestroy, OnInit} from '@angular/core';
import {UserViewService} from '../user-view.service';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {UserModel, UsersService} from 'myportal-api';
import {catchError, map} from 'rxjs/operators';
import {AlertService} from '../../../../../_services/alert.service';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';

@Component({
  selector: 'app-user-reset-password',
  templateUrl: './user-reset-password.component.html',
  styleUrls: ['./user-reset-password.component.css']
})
export class UserResetPasswordComponent implements OnInit, OnDestroy {

  resetPasswordForm = new FormGroup({
    password: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl('', [Validators.required])
  });

  get password(): AbstractControl {
    return this.resetPasswordForm.get('password');
  }

  get confirmPassword(): AbstractControl {
    return this.resetPasswordForm.get('confirmPassword');
  }

  constructor(private viewService: UserViewService, private userService: UsersService, private alertService: AlertService) {
  }

  resetPassword(): void {
    this.resetPasswordForm.markAllAsTouched();
    if (this.resetPasswordForm.invalid) {
      this.alertService.error('Please review the errors and try again.');
      return;
    }
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

  goBack(): void {
    this.resetPasswordForm.reset();
    this.viewService.showDetails();
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.resetPasswordForm.reset();
  }
}
