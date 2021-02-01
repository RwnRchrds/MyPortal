import {Component, OnDestroy, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup} from '@angular/forms';
import {UserModel, UsersService} from 'myportal-api';
import {AppService} from '../../../../../_services/app.service';
import {Router} from '@angular/router';
import {AlertService} from '../../../../../_services/alert.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';
import {AuthService} from '../../../../../_services/auth.service';
import {AppPermissions} from '../../../../../_constants/app-permissions';

@Component({
  selector: 'app-user-search',
  templateUrl: './user-search.component.html',
  styleUrls: ['./user-search.component.css']
})
export class UserSearchComponent implements OnInit, OnDestroy {

  componentName = '#user_search';

  searchForm = new FormGroup({
    username: new FormControl('')
  });

  get username(): AbstractControl {
    return this.searchForm.get('username');
  }

  get table(): any {
    // @ts-ignore
    return $('#search_results').DataTable();
  }

  get allowEditUsers(): boolean {
    return this.authService.hasPermission([AppPermissions.SYSTEM_USERS_EDIT]);
  }

  tableLoaded = false;

  searchResults: UserModel[];

  constructor(private appService: AppService, private userService: UsersService, private router: Router,
              private alertService: AlertService, private authService: AuthService) {
  }

  ngOnInit(): void {
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

  search(): void {
    this.appService.blockComponent(this.componentName);
    try {
      this.userService.getUsers(this.username.value).pipe(map((results: UserModel[]) => {
        this.searchResults = results;
        if (!this.tableLoaded) {
          this.loadTable();
        }
        else {
          this.refreshTable();
        }
      }), catchError((err: HttpErrorResponse) => {
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
    }
    finally {
      this.appService.unblockComponent(this.componentName);
    }
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
