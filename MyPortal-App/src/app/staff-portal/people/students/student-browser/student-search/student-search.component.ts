import { Router } from '@angular/router';
import { AlertService } from './../../../../../_services/alert.service';
import { StudentDataGridModel } from '../../../../../_models/datagrid/student-datagrid';
import { StudentSearch } from '../../../../../_models/search/student-search';
import { Component, OnInit } from '@angular/core';
import {StudentService} from '../../../../../_services/student.service';

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html',
  styleUrls: ['./student-search.component.css']
})
export class StudentSearchComponent implements OnInit {

  searchModel: StudentSearch;

  tableLoaded: boolean;

  searchResults: StudentDataGridModel[];

  constructor(private studentService: StudentService, private alertService: AlertService, private router: Router) {
    this.searchModel = new StudentSearch();
    this.tableLoaded = false;
   }

   search(): void {
    this.studentService.search(this.searchModel).subscribe(next => {
      this.searchResults = next;

      if (!this.tableLoaded)
      {
        this.loadTable();
      }
      else {
        this.refreshTable();
      }
    }, error => {
      this.alertService.error(error);
    });
   }

  refreshTable(): void {
    // @ts-ignore
    const table = $('#student_search_results').DataTable();

    table.clear();
    table.rows.add(this.searchResults);
    table.draw();
  }

   loadTable(): void {
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

  ngOnInit(): void {
  }

}
