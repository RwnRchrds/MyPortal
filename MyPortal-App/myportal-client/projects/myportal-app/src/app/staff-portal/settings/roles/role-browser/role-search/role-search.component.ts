import {Component, OnDestroy, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup} from '@angular/forms';
import {RoleModel, RolesService} from 'myportal-api';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';
import {AppPermissions} from '../../../../../_constants/app-permissions';
import {BaseFormDirective} from '../../../../../_directives/base-form/base-form.directive';

@Component({
  selector: 'app-role-search',
  templateUrl: './role-search.component.html',
  styleUrls: ['./role-search.component.css']
})
export class RoleSearchComponent extends BaseFormDirective implements OnInit, OnDestroy {

  get roleDesc(): AbstractControl {
    return this.form.get('roleDesc');
  }

  get table(): any {
    // @ts-ignore
    return $('#search_results').DataTable();
  }

  get allowEditRoles(): boolean {
    return this.hasPermission([AppPermissions.SYSTEM_GROUPS_EDIT]);
  }

  tableLoaded = false;

  searchResults: RoleModel[];

  constructor(private roleService: RolesService) {
    super();
  }

  ngOnInit(): void {
    this.populatePermissions();
    this.componentName = 'role_search';
    this.form = new FormGroup({
      roleDesc: new FormControl('')
    });
  }

  ngOnDestroy(): void {
    this.unloadTable();
  }

  unloadTable(): void {
    if (this.tableLoaded) {
      this.table.destroy();
      this.tableLoaded = false;
    }
  }

  newRole(): void {
    this.unloadTable();
    this.router.navigate(['/staff/settings/roles/new-role']);
  }

  submit(): void {
    this.blockComponent();
    this.roleService.getRoles(this.roleDesc.value).pipe(map((searchResults => {
      this.searchResults = searchResults;

      if (!this.tableLoaded) {
        this.loadTable();
        this.unblockComponent();
      } else {
        this.refreshTable();
        this.unblockComponent();
      }
    })), catchError((err: HttpErrorResponse) => {
      this.alertService.error(err.error);
      return throwError(err);
    })).subscribe();
  }


  refreshTable(): void {
    const table = this.table;

    table.clear();
    table.rows.add(this.searchResults);
    table.draw();
  }

  rowClickHandler(data: any): void {
    this.router.navigate(['staff/settings/roles/' + data.id]);
  }

  loadTable(): void {
    // @ts-ignore
    $('#search_results').DataTable({
      responsive: true,
      paging: true,
      data: this.searchResults,
      dom: 'rtilp',
      columns: [
        {
          data: 'description',
          render(data, type, role) {
            return `<div class="d-flex align-items-center">
<div class="symbol symbol-40 symbol-light-primary">
<div class="symbol-label font-size-h5">
<i class="fas fa-fw fa-users text-primary"></i>
</div>
</div>
<div class="ml-3">
<div class="text-dark-75 font-weight-bold line-height-sm d-block">${role.description}</div>
</div>
</div>`;
          }
        }
      ],
      rowCallback: (row, data, index) => {
        const self = this;
        // @ts-ignore
        $('td', row).unbind('dblclick');
        // @ts-ignore
        $('td', row).bind('dblclick', () => {
          self.rowClickHandler(data);
        });

        return row;
      },
      language:
        {
          emptyTable: 'No roles'
        }
    });

    // @ts-ignore
    $('#searchResults').removeClass('d-none');
    this.tableLoaded = true;
  }
}
