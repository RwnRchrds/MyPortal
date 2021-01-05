import {Injectable} from '@angular/core';
import {RoleModel, RolesService, TreeNode} from 'myportal-api';
import {Router} from '@angular/router';
import {AppService} from '../../../../_services/app.service';
import {AbstractControl, FormControl, FormGroup} from '@angular/forms';
import {AlertService} from '../../../../_services/alert.service';

@Injectable({
  providedIn: 'root'
})
export class RoleViewService {

  role: RoleModel;
  permissionTree: TreeNode;

  detailsForm = new FormGroup({
    code: new FormControl(''),
    name: new FormControl('')
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
    this.roleService.getRoleById(roleId).subscribe(next => {
      this.role = next;
      this.appService.unblockPage();
      this.code.setValue(this.role.name);
      this.name.setValue(this.role.description);

      if (this.role.system) {
        this.code.disable();
        this.name.disable();
      }
    }, error => {
      this.router.navigate(['']);
    });
    this.roleService.getPermissionsTree(roleId).subscribe(tree => {
      this.permissionTree = tree;
      // @ts-ignore
      $('#perm_tree').jstree({
        plugins: ['wholerow', 'checkbox', 'types'],
        core: {
          themes: {
            responsive: false
          },
          data: [this.permissionTree]
        }
      });
    });
  }

  save(): void {
    this.appService.blockPage();
    // @ts-ignore
    const selectedNodes = $('#perm_tree').jstree('get_selected', true).filter(n => n.children.length === 0);
    console.log(selectedNodes);
    const selectedPermissionIds = selectedNodes.map(n => this.appService.parseGuid(n.id));
    this.roleService.updateRole({id: this.role.id, name: this.code.value,
      description: this.name.value, permissionIds: selectedPermissionIds}).subscribe(result => {
      this.appService.unblockPage();
    }, error => {
        this.alertService.error(error);
        this.appService.unblockPage();
    });
  }

  constructor(protected appService: AppService,
              protected router: Router, private roleService: RolesService, private alertService: AlertService) { }
}
