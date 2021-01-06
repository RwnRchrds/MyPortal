import {Injectable} from '@angular/core';
import {RoleModel, RolesService, TreeNode} from 'myportal-api';
import {Router} from '@angular/router';
import {AppService} from '../../../../_services/app.service';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import { AlertService } from 'projects/myportal-app/src/app/_services/alert.service';
import {PortalViewServiceDirective} from '../../../../shared/portal-view/portal-view-service.directive';
import {AuthService} from '../../../../_services/auth.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleViewService extends PortalViewServiceDirective {

  role: RoleModel;
  permissionTree: TreeNode;

  detailsForm = new FormGroup({
    code: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required])
  });

  get code(): AbstractControl {
    return this.detailsForm.get('code');
  }

  get name(): AbstractControl {
    return this.detailsForm.get('name');
  }

  reset(): void {
    this.role = null;
    this.permissionTree = null;
    this.detailsForm.reset();
  }

  loadModel(roleId: string): void {
    this.appService.blockPage();
    try {
      this.roleService.getRoleById(roleId).pipe(map((role: RoleModel) => {
        this.role = role;
        this.code.setValue(this.role.name);
        this.name.setValue(this.role.description);

        if (this.role.system) {
          this.code.disable();
          this.name.disable();
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

  loadPermissionsTree(): void {
    if (this.role == null)
    {
      return;
    }
    this.roleService.getPermissionsTree(this.role.id).pipe(map((tree: TreeNode) => {
      this.permissionTree = tree;
      // @ts-ignore
      $('#perm_tree').jstree({
        plugins: ['wholerow', 'checkbox', 'types'],
        core: {
          themes: {
            responsive: false
          },
          expand_selected_onload: false,
          data: [this.permissionTree]
        }
      });
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
      // @ts-ignore
      const selectedNodes = $('#perm_tree').jstree('get_selected', true).filter(n => n.children.length === 0);
      console.log(selectedNodes);
      const selectedPermissionIds = selectedNodes.map(n => this.appService.parseGuid(n.id));
      this.roleService.updateRole({id: this.role.id, name: this.code.value,
        description: this.name.value, permissionIds: selectedPermissionIds}).pipe(map(result => {
        this.loadModel(this.role.id);
      }), catchError((err: HttpErrorResponse) => {
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
    }
    finally {
      this.appService.unblockPage();
    }
  }

  delete(): void {
    this.alertService.areYouSure(`Are you sure you want to delete '${this.role.description}'?`).then(userResponse => {
      if (userResponse.isConfirmed) {
        this.appService.blockPage();
        this.roleService.deleteRole(this.role.id).pipe(map(result => {
          this.router.navigate(['/staff/settings/roles']);
          this.appService.unblockPage();
        }), catchError((err: HttpErrorResponse) => {
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      }
    });
  }

  constructor(authService: AuthService, protected appService: AppService,
              protected router: Router, private roleService: RolesService, private alertService: AlertService) {
    super(authService);
  }
}
