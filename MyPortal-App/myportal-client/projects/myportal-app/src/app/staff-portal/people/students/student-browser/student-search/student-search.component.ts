import { Component, OnInit } from '@angular/core';
import {
  HouseModel, HousesService,
  RegGroupModel,
  RegGroupsService,
  StudentDataGridModel,
  StudentsService,
  YearGroupModel,
  YearGroupsService
} from 'myportal-api';
import {AbstractControl, FormControl, FormGroup} from '@angular/forms';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';
import {BaseFormDirective} from '../../../../../_directives/base-form/base-form.directive';

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html',
  styleUrls: ['./student-search.component.css']
})
export class StudentSearchComponent extends BaseFormDirective implements OnInit {

  tableLoaded = false;

  searchResults: StudentDataGridModel[];

  yearGroups: YearGroupModel[];
  regGroups: RegGroupModel[];
  houses: HouseModel[];

  constructor(private studentService: StudentsService, private yearGroupService: YearGroupsService,
              private regGroupService: RegGroupsService, private houseService: HousesService) {
    super();
  }

  ngOnInit(): void {
    this.componentName = 'student_search';
    this.form = new FormGroup({
      studentStatus: new FormControl(1),
      yearGroupId: new FormControl(''),
      regGroupId: new FormControl(''),
      houseId: new FormControl(''),
      firstName: new FormControl(''),
      lastName: new FormControl('')
    });
    this.loadSearchModel();
  }

  loadSearchModel(): void {
    this.yearGroupService.getYearGroups().subscribe(next => {
      this.yearGroups = next;
    });

    this.regGroupService.getRegGroups().subscribe(next => {
      this.regGroups = next;
    });

    this.houseService.getHouses().subscribe(next => {
      this.houses = next;
    });
  }

  get studentStatus(): AbstractControl {
    return this.form.get('studentStatus');
  }

  get yearGroupId(): AbstractControl {
    return this.form.get('yearGroupId');
  }

  get regGroupId(): AbstractControl {
    return this.form.get('regGroupId');
  }

  get houseId(): AbstractControl {
    return this.form.get('houseId');
  }

  get firstName(): AbstractControl {
    return this.form.get('firstName');
  }

  get lastName(): AbstractControl {
    return this.form.get('lastName');
  }

  rowClickHandler(data: any): void {
    this.router.navigate(['staff/students/' + data.id]);
  }

  submit(): void {
    this.blockComponent();
    this.studentService.searchStudents(this.studentStatus.value, null, this.regGroupId.value,
      this.yearGroupId.value, this.houseId.value, null,
      this.firstName.value, this.lastName.value).pipe(map((searchResults: StudentDataGridModel[]) => {
      this.searchResults = searchResults;

      if (!this.tableLoaded)
      {
        this.loadSearchResults();
        this.unblockComponent();
      }
      else {
        this.refreshSearchResults();
        this.unblockComponent();
      }
    }), catchError((err: HttpErrorResponse) => {
      this.alertService.error(err.error);
      return throwError(err);
    })).subscribe();
  }

  refreshSearchResults(): void {
    // @ts-ignore
    const table = $('#student_search_results').DataTable();

    table.clear();
    table.rows.add(this.searchResults);
    table.draw();
  }

  loadSearchResults(): void {
    // @ts-ignore
    $('#student_search_results').DataTable({
      responsive: true,
      paging: true,
      data: this.searchResults,
      dom: 'rtilp',
      columns: [
        {
          data: 'displayName',
          render(data, type, student) {
            return `<div class="d-flex align-items-center">
<div class="symbol symbol-40 symbol-light-primary">
<div class="symbol-label font-size-h5">
<i class="fas fa-fw fa-user-graduate text-primary"></i>
</div>
</div>
<div class="ml-3">
<div class="text-dark-75 font-weight-bold line-height-sm d-block">${student.displayName}</div>
</div>
</div>`;
          }
        },
        {
          data: 'gender',
          render(data, type, student) {
            if (student.gender === 'M') {
              return `<div class="label label-lg font-weight-bold label-light-primary label-inline">Male</div>`;
            } else if (student.gender === 'F') {
              return `<div class="label label-lg font-weight-bold label-light-danger label-inline">Female</div>`;
            } else if (student.gender === 'X') {
              return `<div class="label label-lg font-weight-bold label-light-warning label-inline">Other</div>`;
            } else {
              return `<div class="label label-lg font-weight-bold label-light-secondary label-inline">Unknown</div>`;
            }
          }
        },
        {
          data: 'regGroupName'
        },
        {
          data: 'yearGroupName'
        },
        {
          data: 'houseName'
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
          emptyTable: 'No students'
        }
    });

    // @ts-ignore
    $('#searchResults').removeClass('d-none');
    this.tableLoaded = true;
  }
}
