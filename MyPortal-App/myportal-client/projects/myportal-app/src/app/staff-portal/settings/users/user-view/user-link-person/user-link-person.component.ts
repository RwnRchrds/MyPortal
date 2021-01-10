import {Component, OnDestroy, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup} from '@angular/forms';
import {PersonSearchResultModel, PersonService} from 'myportal-api';
import {UserViewService} from '../user-view.service';
import {AppService} from '../../../../../_services/app.service';
import {AlertService} from '../../../../../_services/alert.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';

@Component({
  selector: 'app-user-link-person',
  templateUrl: './user-link-person.component.html',
  styleUrls: ['./user-link-person.component.css']
})
export class UserLinkPersonComponent implements OnInit, OnDestroy {

  componentName = '#person_search';

  searchForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl('')
  });

  get firstName(): AbstractControl {
    return this.searchForm.get('firstName');
  }

  get lastName(): AbstractControl {
    return this.searchForm.get('lastName');
  }

  get table(): any {
    // @ts-ignore
    return $('#search_results').DataTable();
  }

  tableLoaded = false;

  searchResults: PersonSearchResultModel[];

  viewService: UserViewService;

  constructor(userViewService: UserViewService, private appService: AppService, private personService: PersonService,
              private alertService: AlertService) {
    this.viewService = userViewService;
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.unloadTable();
  }

  goBack(): void {
    this.unloadTable();
    this.searchForm.reset();
    this.viewService.showDetails();
  }

  unloadTable(): void {
    if (this.tableLoaded) {
      this.table.destroy();
      this.tableLoaded = false;
    }
  }

  search(): void {
    this.appService.blockComponent(this.componentName);
    try {
      this.personService.searchPeople(this.firstName.value, this.lastName.value).pipe(map((results: PersonSearchResultModel[]) => {
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
    console.log('Event triggered.');
    this.alertService.areYouSure(`Are you sure you wish to link user '${this.viewService.user.userName}' to '${data.person.lastName}, ${data.person.firstName}'?`)
      .then(result => {
        if (result.isConfirmed) {
          if (data.personTypes.isUser) {
            this.alertService.error('This person is already linked to a user.');
            return;
          }
          this.viewService.setLinkedPerson(data.person);
          this.goBack();
        }
      });
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
          data: 'person.lastName',
          render(data, type, person) {
            return `<div class="d-flex align-items-center">
<div class="symbol symbol-40 symbol-light-primary">
<div class="symbol-label font-size-h5">
<i class="fas fa-fw fa-user text-primary"></i>
</div>
</div>
<div class="ml-3">
<div class="text-dark-75 font-weight-bold line-height-sm d-block">${person.person.lastName}, ${person.person.firstName}</div>
</div>
</div>`;
          }
        },
        {
          data: 'personTypes',
          render(data, type, person) {
            let output = '';
            if (person.personTypes.isStaff) {
              output = `${output}<div class="label label-lg font-weight-bold label-light-primary label-inline">Staff</div>`;
            }
            if (person.personTypes.isContact) {
              output = `${output}<div class="label label-lg font-weight-bold label-light-primary label-inline">Contact</div>`;
            }
            if (person.personTypes.isStudent) {
              output = `${output}<div class="label label-lg font-weight-bold label-light-primary label-inline">Student</div>`;
            }
            if (person.personTypes.isAgent) {
              output = `${output}<div class="label label-lg font-weight-bold label-light-primary label-inline">Agent</div>`;
            }
            if (person.personTypes.isApplicant) {
              output = `${output}<div class="label label-lg font-weight-bold label-light-primary label-inline">Applicant</div>`;
            }
            return output;
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
          emptyTable: 'No people'
        }
    });

    // @ts-ignore
    $('#searchResults').removeClass('d-none');
    this.tableLoaded = true;
  }
}
