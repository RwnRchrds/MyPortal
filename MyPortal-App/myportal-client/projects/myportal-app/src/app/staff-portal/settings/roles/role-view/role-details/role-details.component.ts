import {Component, OnDestroy, OnInit} from '@angular/core';
import {RoleModel, RolesService, TreeNode} from 'myportal-api';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {Subscription, throwError} from 'rxjs';
import {ScriptService} from '../../../../../_services/script.service';
import {ActivatedRoute} from '@angular/router';
import {RoleViewService} from '../role-view.service';
import {BaseFormDirective} from '../../../../../_directives/base-form/base-form.directive';

@Component({
  selector: 'app-role-details',
  templateUrl: './role-details.component.html',
  styleUrls: ['./role-details.component.css']
})
export class RoleDetailsComponent extends BaseFormDirective implements OnInit, OnDestroy {

  role: RoleModel;
  permissionTree: TreeNode;
  roleSubscription: Subscription;
  permissionsSubscription: Subscription;

  constructor(private scriptService: ScriptService, private roleService: RolesService,
              private route: ActivatedRoute, private viewService: RoleViewService) {
    super();
  }

  ngOnInit(): void {
    this.form = new FormGroup({
      code: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required])
    });
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
    this.roleSubscription = this.viewService.currentRole.pipe(map((role: RoleModel) => {
      this.role = role;
      this.code.setValue(role.name);
      this.name.setValue(role.description);
    })).subscribe();
    this.permissionsSubscription = this.viewService.currentRolePermissions.pipe(map((permissionsTree: TreeNode) => {
      this.permissionTree = permissionsTree;
      // @ts-ignore
      const treeElement = $('#perm_tree');
      treeElement.jstree().settings.core.data = this.permissionTree;
      treeElement.jstree().refresh(true);
      treeElement.on('refresh.jstree', () => {
        this.selectNode(treeElement, this.permissionTree);
      });
    })).subscribe();
  }

  selectNode(treeElement: any, node: TreeNode): void {
    if (node.children.length === 0) {
      if (node.state.selected) {
        treeElement.jstree('select_node', node.id);
      }
    }
    else {
      for (const child of node.children) {
        this.selectNode(treeElement, child);
      }
    }
  }

  ngOnDestroy(): void {
    this.roleSubscription.unsubscribe();
    this.permissionsSubscription.unsubscribe();
  }

  get code(): AbstractControl {
    return this.form.get('code');
  }

  get name(): AbstractControl {
    return this.form.get('name');
  }

  reset(): void {
    this.role = null;
    this.permissionTree = null;
    this.form.reset();
  }

  submit(): void {
    if (this.validate()) {
      this.blockComponent();
      // @ts-ignore
      const selectedNodes = $('#perm_tree').jstree('get_selected', true).filter(n => n.children.length === 0);
      console.log(selectedNodes);
      const selectedPermissions = selectedNodes.map((n: TreeNode) => parseInt(n.id, 10));
      this.roleService.updateRole({
        id: this.role.id,
        description: this.name.value,
        name: this.code.value,
        permissionValues: selectedPermissions
      }).pipe(map(result => {
        this.alertService.toastSuccess('Role updated');
        this.viewService.reload();
      }), catchError((err: HttpErrorResponse) => {
        this.unblockComponent();
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
    }
  }

  delete(): void {
    this.alertService.areYouSure(`Are you sure you want to delete '${this.role.description}'?`).then(userResponse => {
      if (userResponse.isConfirmed) {
        this.appService.blockPage();
        this.roleService.deleteRole(this.role.id).pipe(map(result => {
          this.alertService.toastSuccess('Role deleted');
          this.router.navigate(['/staff/settings/roles']);
          this.appService.unblockPage();
        }), catchError((err: HttpErrorResponse) => {
          this.appService.unblockPage();
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      }
    });
  }
}
