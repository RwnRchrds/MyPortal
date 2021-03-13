import {Component, OnDestroy, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup} from '@angular/forms';
import {UserModel, UsersService} from 'myportal-api';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';
import {AppPermissions} from '../../../../../_constants/app-permissions';
import {BaseFormDirective} from '../../../../../_directives/base-form/base-form.directive';

@Component({
  selector: 'app-user-search',
  templateUrl: './user-search.component.html',
  styleUrls: ['./user-search.component.css']
})
export class UserSearchComponent extends BaseFormDirective implements OnInit, OnDestroy {

  componentName = '#user_search';

  get username(): AbstractControl {
    return this.form.get('username');
  }

  get table(): any {
    // @ts-ignore
    return $('#search_results').DataTable();
  }

  get allowEditUsers(): boolean {
    return this.hasPermission([AppPermissions.SYSTEM_USERS_EDIT]);
  }

  tableLoaded = false;

  searchResults: UserModel[];

  constructor(private userService: UsersService) {
    super();
  }

  ngOnInit(): void {
    this.populatePermissions();
    this.componentName = 'user_search';
    this.form = new FormGroup({
      username: new FormControl('')
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

  newUser(): void {
    this.unloadTable();
    this.router.navigate(['/staff/settings/users/new-user']);
  }

  submit(): void {
    this.blockComponent();
    this.userService.getUsers(this.username.value).pipe(map((results: UserModel[]) => {
      this.searchResults = results;
      if (!this.tableLoaded) {
        this.loadTable();
        this.unblockComponent();
      }
      else {
        this.refreshTable();
        this.unblockComponent();
      }
    }), catchError((err: HttpErrorResponse) => {
      this.unblockComponent();
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
    this.router.navigate(['staff/settings/users/' + data.id]);
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
          data: 'userName',
          render(data, type, user) {
            return `<div class="d-flex align-items-center">
<div class="symbol symbol-40 symbol-light-primary">
<div class="symbol-label font-size-h5">
<i class="fas fa-fw fa-user text-primary"></i>
</div>
</div>
<div class="ml-3">
<div class="text-dark-75 font-weight-bold line-height-sm d-block">${user.userName}</div>
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
          emptyTable: 'No users'
        }
    });

    // @ts-ignore
    $('#searchResults').removeClass('d-none');
    this.tableLoaded = true;
  }
}
