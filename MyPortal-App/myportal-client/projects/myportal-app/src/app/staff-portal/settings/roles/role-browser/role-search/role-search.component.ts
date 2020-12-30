import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { RoleModel, RolesService } from 'myportal-api';
import { AlertService } from 'projects/myportal-app/src/app/_services/alert.service';
import {AppService} from '../../../../../_services/app.service';

@Component({
  selector: 'app-role-search',
  templateUrl: './role-search.component.html',
  styleUrls: ['./role-search.component.css']
})
export class RoleSearchComponent implements OnInit {

  componentName = '#role_search';

  searchForm = new FormGroup({
    roleDesc: new FormControl('')
  });

  get roleDesc(): AbstractControl {
    return this.searchForm.get('roleDesc');
  }

  tableLoaded = false;

  searchResults: RoleModel[];

  constructor(private appService: AppService, private roleService: RolesService, private router: Router,
              private alertService: AlertService) { }

  search(): void {
    this.appService.blockComponent(this.componentName);
    this.roleService.getRoles(this.roleDesc.value).subscribe(next => {
      this.searchResults = next;

      if (!this.tableLoaded)
      {
        this.loadTable();
        this.appService.unblockComponent(this.componentName);
      }
      else {
        this.refreshTable();
        this.appService.unblockComponent(this.componentName);
      }
    }, error => {
      this.alertService.error(error);
      this.appService.unblockComponent(this.componentName);
    });
  }

  refreshTable(): void {
    // @ts-ignore
    const table = $('#search_results').DataTable();

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

  ngOnInit(): void {
  }

}
