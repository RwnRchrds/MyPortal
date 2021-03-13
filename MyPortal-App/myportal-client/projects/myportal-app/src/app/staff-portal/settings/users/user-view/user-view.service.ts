import { Injectable } from '@angular/core';
import {PortalViewServiceDirective} from '../../../../_directives/portal-view/portal-view-service.directive';
import {AuthService} from '../../../../_services/auth.service';
import {RoleModel, RolesService, UserModel, UsersService} from 'myportal-api';
import {AppService} from '../../../../_services/app.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {AlertService} from '../../../../_services/alert.service';
import {BehaviorSubject, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserViewService extends PortalViewServiceDirective {

  private showDetailsSource: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);
  private showLinkPersonSource: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private showResetPasswordSource: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private userSource: BehaviorSubject<UserModel> = new BehaviorSubject<UserModel>(null);
  private rolesSource: BehaviorSubject<RoleModel[]> = new BehaviorSubject<RoleModel[]>(null);
  private userRolesSource: BehaviorSubject<string[]> = new BehaviorSubject<string[]>(null);
  currentShowDetails = this.showDetailsSource.asObservable();
  currentLinkPerson = this.showLinkPersonSource.asObservable();
  currentShowResetPassword = this.showResetPasswordSource.asObservable();
  currentUser = this.userSource.asObservable();
  currentRoles = this.rolesSource.asObservable();
  currentUserRoles = this.userRolesSource.asObservable();

  showLinkPerson(): void {
    this.showDetailsSource.next(false);
    this.showResetPasswordSource.next(false);
    this.showLinkPersonSource.next(true);
  }

  showResetPassword(): void {
    this.showDetailsSource.next(false);
    this.showLinkPersonSource.next(false);
    this.showResetPasswordSource.next(true);
  }

  showDetails(): void {
    this.showLinkPersonSource.next(false);
    this.showResetPasswordSource.next(false);
    this.showDetailsSource.next(true);
  }

  constructor(authService: AuthService, private appService: AppService, private userService: UsersService,
              private alertService: AlertService, private roleService: RolesService) {
    super(authService);
  }

  reset(): void {
    this.userSource.next(null);
    this.rolesSource.next(null);
    this.userRolesSource.next(null);
    this.showDetails();
  }

  updateUser(user: UserModel): void {
    this.userSource.next(user);
  }

  init(userId: string): void {
    this.appService.blockPage();
    this.userService.getUserById(userId).pipe(map((user: UserModel) => {
      this.userSource.next(user);
      this.roleService.getRoles().pipe(map((roles: RoleModel[]) => {
        this.rolesSource.next(roles);
      })).subscribe();
      this.userService.getUserRoles(user.id).pipe(map((userRoles: RoleModel[]) => {
        this.appService.unblockPage();
        this.userRolesSource.next(userRoles.map(ur => ur.id));
      })).subscribe();
    }), catchError((err: HttpErrorResponse) => {
      this.appService.unblockPage();
      this.alertService.error(err.error);
      return throwError(err);
    })).subscribe();
  }

  reload(): void {
    const sub = this.userSource.pipe(map((user: UserModel) => {
      if (user == null) {
        return;
      }
      this.init(user.id);
    })).subscribe();
    sub.unsubscribe();
  }
}
