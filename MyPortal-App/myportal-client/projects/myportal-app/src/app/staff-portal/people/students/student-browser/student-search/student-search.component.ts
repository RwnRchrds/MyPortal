import { Component, OnInit } from '@angular/core';
import {StudentBrowserService} from '../student-browser.service';
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
import {AlertService} from '../../../../../_services/alert.service';
import {AppService} from '../../../../../_services/app.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html',
  styleUrls: ['./student-search.component.css']
})
export class StudentSearchComponent implements OnInit {

  viewService: StudentBrowserService;

  searchComponentName = '#student_search';

  tableLoaded = false;

  searchResults: StudentDataGridModel[];

  yearGroups: YearGroupModel[];
  regGroups: RegGroupModel[];
  houses: HouseModel[];

  searchForm = new FormGroup({
    studentStatus: new FormControl(1),
    yearGroupId: new FormControl(''),
    regGroupId: new FormControl(''),
    houseId: new FormControl(''),
    firstName: new FormControl(''),
    lastName: new FormControl('')
  });

  constructor(studentBrowserService: StudentBrowserService, private studentService: StudentsService, private alertService: AlertService,
              private appService: AppService, private yearGroupService: YearGroupsService,
              private regGroupService: RegGroupsService, private houseService: HousesService,
              private router: Router) {
    this.viewService = studentBrowserService;
   }

  ngOnInit(): void {
    this.loadSearchModel();
  }

  get studentStatus(): AbstractControl {
    return this.searchForm.get('studentStatus');
  }

  get yearGroupId(): AbstractControl {
    return this.searchForm.get('yearGroupId');
  }

  get regGroupId(): AbstractControl {
    return this.searchForm.get('regGroupId');
  }

  get houseId(): AbstractControl {
    return this.searchForm.get('houseId');
  }

  get firstName(): AbstractControl {
    return this.searchForm.get('firstName');
  }

  get lastName(): AbstractControl {
    return this.searchForm.get('lastName');
  }

  search(): void {
    this.appService.blockComponent(this.searchComponentName);
    this.studentService.searchStudents(this.studentStatus.value, null, this.regGroupId.value,
      this.yearGroupId.value, this.houseId.value, null, this.firstName.value, this.lastName.value).subscribe(next => {
      this.searchResults = next;

      if (!this.tableLoaded)
      {
        this.loadSearchResults();
        this.appService.unblockComponent(this.searchComponentName);
      }
      else {
        this.refreshSearchResults();
        this.appService.unblockComponent(this.searchComponentName);
      }
    }, error => {
      this.alertService.error(error);
      this.appService.unblockComponent(this.searchComponentName);
    });
  }

  refreshSearchResults(): void {
    // @ts-ignore
    const table = $('#student_search_results').DataTable();

    table.clear();
    table.rows.add(this.searchResults);
    table.draw();
  }

  loadSearchResults(): void {
    const tempOverviewUrl = this.router.createUrlTree([`staff/students/tempid`]).toString();

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
            const overviewUrl = tempOverviewUrl.replace('tempid', student.id);
            return `<div class="d-flex align-items-center"><div class="symbol symbol-40 symbol-light-primary"><div class="symbol-label font-size-h5"><i class="fas fa-fw fa-user-graduate text-primary"></i></div></div><div class="ml-3"><div class="text-dark-75 font-weight-bold line-height-sm d-block"><a href="${
              overviewUrl}">${student.displayName}</a></div></div></div>`;
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
      language:
        {
          emptyTable: 'No students'
        }
    });

    // @ts-ignore
    $('#searchResults').removeClass('d-none');
    this.tableLoaded = true;
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
}
