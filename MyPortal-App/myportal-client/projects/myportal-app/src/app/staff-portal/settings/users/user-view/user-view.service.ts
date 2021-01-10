import { Injectable } from '@angular/core';
import {PortalViewServiceDirective} from '../../../../shared/portal-view/portal-view-service.directive';
import {AuthService} from '../../../../_services/auth.service';
import {PersonModel, RoleModel, RolesService, UserModel, UsersService} from 'myportal-api';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {AppService} from '../../../../_services/app.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {AlertService} from '../../../../_services/alert.service';
import {throwError} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserViewService extends PortalViewServiceDirective {

  showDetailsComponent = true;
  showLinkPersonComponent = false;
  showResetPasswordComponent = false;

  user: UserModel;
  roles: RoleModel[];

  detailsForm = new FormGroup({
    username: new FormControl({value: '', disabled: true}, [Validators.required]),
    userType: new FormControl({value: '', disabled: true}, [Validators.required]),
    roles: new FormControl(''),
    personName: new FormControl({value: '', disabled: true})
  });

  showLinkPerson(): void {
    this.showDetailsComponent = false;
    this.showResetPasswordComponent = false;
    this.showLinkPersonComponent = true;
  }

  showResetPassword(): void {
    this.showDetailsComponent = false;
    this.showLinkPersonComponent = false;
    this.showResetPasswordComponent = true;
  }

  showDetails(): void {
    this.showLinkPersonComponent = false;
    this.showResetPasswordComponent = false;
    this.showDetailsComponent = true;
  }

  get username(): AbstractControl {
    return this.detailsForm.get('username');
  }

  get userType(): AbstractControl {
    return this.detailsForm.get('userType');
  }

  get userRoles(): AbstractControl {
    return this.detailsForm.get('roles');
  }

  get personName(): AbstractControl {
    return this.detailsForm.get('personName');
  }

  constructor(authService: AuthService, private appService: AppService, private userService: UsersService,
              private alertService: AlertService, private roleService: RolesService) {
    super(authService);
  }

  reset(): void {
    this.user = null;
    this.roles = null;
    this.detailsForm.reset();

    this.showDetails();
  }

  setLinkedPerson(person: PersonModel): void {
    this.user.person = person;
    this.user.personId = person.id;
    this.personName.setValue(`${person.lastName}, ${person.firstName}`);
  }

  removeLinkedPerson(): void {
    this.user.person = null;
    this.user.personId = null;
    this.personName.setValue('');
  }

  loadModel(userId: string): void {
    this.appService.blockPage();
    try {
      this.userService.getUserById(userId).pipe(map((user: UserModel) => {
        this.user = user;
        this.username.setValue(this.user.userName);
        this.userType.setValue(this.user.userType.toString());
        if (this.user.person != null)
        {
          this.personName.setValue(`${this.user.person.lastName}, ${this.user.person.firstName}`);
        }
      }), catchError((err: HttpErrorResponse) => {
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
    }
    finally {
      this.appService.unblockPage();
    }
  }

  loadRoles(): void {
    if (this.user == null) {
      return;
    }

    this.roleService.getRoles().pipe(map((roles: RoleModel[]) => {
      this.roles = roles;
      this.userService.getUserRoles(this.user.id).pipe(map((userRoles: RoleModel[]) => {
        this.userRoles.setValue(userRoles.map(u => u.id));
      })).subscribe();
    }), catchError((err: HttpErrorResponse) => {
      this.alertService.error(err.error);
      return throwError(err);
    })).subscribe();
  }

  save(): void {
    this.detailsForm.markAllAsTouched();
    this.appService.blockPage();
    try {
      if (this.detailsForm.invalid) {
        this.alertService.error('Please review the errors and try again.');
        return;
      }

      this.userService.updateUser({id: this.user.id, personId: this.user.personId, roleIds: this.userRoles.value}).pipe(map(result => {
        this.loadModel(this.user.id);
      }), catchError((err: HttpErrorResponse) => {
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
    }
    finally {
      this.appService.unblockPage();
    }
  }
}
