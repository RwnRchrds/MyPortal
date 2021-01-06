import {Component, OnDestroy, OnInit} from '@angular/core';
import {RoleBrowserService} from '../role-browser.service';
import {AbstractControl, FormControl, FormGroup} from '@angular/forms';
import {RoleModel, RolesService} from 'myportal-api';
import {AppService} from '../../../../../_services/app.service';
import {Router} from '@angular/router';
import {AlertService} from '../../../../../_services/alert.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';

@Component({
  selector: 'app-role-search',
  templateUrl: './role-search.component.html',
  styleUrls: ['./role-search.component.css']
})
export class RoleSearchComponent implements OnInit, OnDestroy {

  componentName = '#role_search';

  searchForm = new FormGroup({
    roleDesc: new FormControl('')
  });

  get roleDesc(): AbstractControl {
    return this.searchForm.get('roleDesc');
  }

  get table(): any {
    // @ts-ignore
    return $('#search_results').DataTable();
  }

  tableLoaded = false;

  searchResults: RoleModel[];

  viewService: RoleBrowserService;

  constructor(roleBrowserService: RoleBrowserService, private appService: AppService,
              private roleService: RolesService, private router: Router,
              private alertService: AlertService) {
    this.viewService = roleBrowserService;
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

  newRole(): void {
    this.unloadTable();
    this.viewService.showCreate();
  }

  search(): void {
    this.appService.blockComponent(this.componentName);
    this.roleService.getRoles(this.roleDesc.value).pipe(map((searchResults => {
      this.searchResults = searchResults;

      if (!this.tableLoaded) {
        this.loadTable();
        this.appService.unblockComponent(this.componentName);
      } else {
        this.refreshTable();
        this.appService.unblockComponent(this.componentName);
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

  loadTable(): void {
    const tempOverviewUrl = this.router.createUrlTree([`staff/settings/roles/tempid`]).toString();

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
            const overviewUrl = tempOverviewUrl.replace('tempid', role.id);
            return `<div class="d-flex align-items-center"><div class="symbol symbol-40 symbol-light-primary"><div class="symbol-label font-size-h5"><i class="fas fa-fw fa-users text-primary"></i></div></div><div class="ml-3"><div class="text-dark-75 font-weight-bold line-height-sm d-block"><a href="${
              overviewUrl}">${role.description}</a></div></div></div>`;
          }
        }
      ],
      language:
        {
          emptyTable: 'No students'
        }
    });

    // @ts-ignore
    $('#searchResults').removeClass('d-none');
    this.tableLoaded = true;
  }
}
