import { Injectable } from '@angular/core';
import {BehaviorSubject, throwError} from 'rxjs';
import {RoleModel, RolesService, TreeNode} from 'myportal-api';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {AlertService} from '../../../../_services/alert.service';

@Injectable({
  providedIn: 'root'
})
export class RoleViewService {

  private roleSource: BehaviorSubject<RoleModel> = new BehaviorSubject<RoleModel>(null);
  private permissionsSource: BehaviorSubject<TreeNode> = new BehaviorSubject<TreeNode>(null);
  currentRole = this.roleSource.asObservable();
  currentRolePermissions = this.permissionsSource.asObservable();

  constructor(private roleService: RolesService, private alertService: AlertService) { }

  init(roleId: string): void {
    this.roleService.getRoleById(roleId).pipe(map((roleModel: RoleModel) => {
      this.roleSource.next(roleModel);
      this.roleService.getPermissionsTree(roleModel.id).pipe(map((permissionsTree: TreeNode) => {
        this.permissionsSource.next(permissionsTree);
      })).subscribe();
    }), catchError((err: HttpErrorResponse) => {
      this.alertService.error(err.error);
      return throwError(err);
    })).subscribe();
  }

  reset(): void {
    this.roleSource.next(null);
    this.permissionsSource.next(null);
  }

  reload(): void {
    const sub = this.roleSource.subscribe(role => {
      this.init(role.id);
    });

    sub.unsubscribe();
  }

  updateRole(role: RoleModel): void {
    this.roleSource.next(role);
  }
}
