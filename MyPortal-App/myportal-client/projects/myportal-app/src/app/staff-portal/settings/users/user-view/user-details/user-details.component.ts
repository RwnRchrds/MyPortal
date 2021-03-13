import {Component, OnDestroy, OnInit} from '@angular/core';
import {UserViewService} from '../user-view.service';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {Subscription, throwError} from 'rxjs';
import {RoleModel, UserModel, UsersService} from 'myportal-api';
import {SweetAlertResult} from '../../../../../_models/sweetAlertResult';
import {BaseFormDirective} from '../../../../../_directives/base-form/base-form.directive';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent extends BaseFormDirective implements OnInit, OnDestroy {

  user: UserModel;
  roles: RoleModel[];
  userSubscription: Subscription;
  rolesSubscription: Subscription;
  userRolesSubscription: Subscription;

  constructor(private viewService: UserViewService, private userService: UsersService) {
    super();
  }

  ngOnInit(): void {
    this.componentName = 'user_details';
    this.form = new FormGroup({
      username: new FormControl({value: '', disabled: true}, [Validators.required]),
      userType: new FormControl({value: '', disabled: true}, [Validators.required]),
      roles: new FormControl(''),
      personName: new FormControl({value: '', disabled: true})
    });
    this.userSubscription = this.viewService.currentUser.pipe(map((user: UserModel) => {
      this.user = user;
      this.username.setValue(this.user.userName);
      this.userType.setValue(this.user.userType.toString());
      if (this.user.person)
      {
        this.personName.setValue(`${this.user.person.lastName}, ${this.user.person.firstName}`);
      }
    })).subscribe();
    this.rolesSubscription = this.viewService.currentRoles.pipe(map((roles: RoleModel[]) => {
      this.roles = roles;
      this.userRolesSubscription = this.viewService.currentUserRoles.pipe(map((userRoles: string[]) => {
        this.userRoles.setValue(userRoles);
        console.log(userRoles);
      })).subscribe();
    })).subscribe();
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
    this.rolesSubscription.unsubscribe();
    this.userRolesSubscription.unsubscribe();
  }

  get username(): AbstractControl {
    return this.form.get('username');
  }

  get userType(): AbstractControl {
    return this.form.get('userType');
  }

  get userRoles(): AbstractControl {
    return this.form.get('roles');
  }

  get personName(): AbstractControl {
    return this.form.get('personName');
  }

  removeLinkedPerson(): void {
    this.user.personId = null;
    this.user.person = null;
    this.personName.setValue('');
    this.viewService.updateUser(this.user);
  }

  showLinkPerson(): void {
    this.viewService.showLinkPerson();
  }

  showResetPassword(): void {
    this.viewService.showResetPassword();
  }

  setUserEnabled(enabled: boolean): void {
    const verb = enabled ? 'enable' : 'disable';
    this.alertService.areYouSure(`Are you sure you want to ${verb} user ${this.user.userName}?`).then((result: SweetAlertResult) => {
      if (result.isConfirmed) {
        this.appService.blockPage();
        this.userService.setEnabled({userId: this.user.id, enabled}).pipe(map((enabledResult) => {
          this.viewService.reload();
          this.appService.unblockPage();
        }), catchError((err: HttpErrorResponse) => {
          this.appService.unblockPage();
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      }
    });
  }

  delete(): void {
    this.alertService.areYouSure(`Are you sure you want to delete user ${this.user.userName}?`).then((result: SweetAlertResult) => {
      if (result.isConfirmed) {
        this.userService.deleteUser(this.user.id).pipe(map(deleteResult => {
          this.alertService.toastSuccess('User deleted');
          this.router.navigate(['/staff/settings/users']);
        }), catchError((err: HttpErrorResponse) => {
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      }
    });
  }

  submit(): void {
    if (this.validate()) {
      this.userService.updateUser({id: this.user.id, personId: this.user.personId, roleIds: this.userRoles.value})
        .pipe(map(result => {
          this.alertService.toastSuccess('User saved');
          this.viewService.reload();
        }), catchError((err: HttpErrorResponse) => {
          this.appService.unblockPage();
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
    }
  }
}
