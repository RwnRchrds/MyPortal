import {Component, OnDestroy, OnInit} from '@angular/core';
import {RoleModel, RolesService, TreeNode} from 'myportal-api';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {Subscription, throwError} from 'rxjs';
import {ScriptService} from '../../../../../_services/script.service';
import {AppService} from '../../../../../_services/app.service';
import {ActivatedRoute, Router} from '@angular/router';
import {AlertService} from '../../../../../_services/alert.service';
import {RoleViewService} from '../role-view.service';

@Component({
  selector: 'app-role-details',
  templateUrl: './role-details.component.html',
  styleUrls: ['./role-details.component.css']
})
export class RoleDetailsComponent implements OnInit, OnDestroy {

  role: RoleModel;
  permissionTree: TreeNode;
  roleSubscription: Subscription;
  permissionsSubscription: Subscription;

  detailsForm = new FormGroup({
    code: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required])
  });

  constructor(private scriptService: ScriptService, private appService: AppService,
              protected router: Router, private roleService: RolesService, private alertService: AlertService,
              private route: ActivatedRoute, private viewService: RoleViewService) {
  }

  ngOnInit(): void {
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
        this.viewService.reload();
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
          this.appService.unblockPage();
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      }
    });
  }
}
