import { StudentSearch } from './../../../../../_models/People/Students/student-search';
import { Component, OnInit } from '@angular/core';
import {StudentService} from '../../../../../_services/student.service';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html',
  styleUrls: ['./student-search.component.css']
})
export class StudentSearchComponent implements OnInit {

  searchModel: StudentSearch;

  showResults: boolean;

  constructor(private studentService: StudentService) {
    this.searchModel = new StudentSearch();
    this.showResults = false;
   }

   search(): void {
    this.studentService.search(this.searchModel).subscribe(next => {
      console.log('Success');
    }, error => {
      console.log(error);
    });
   }

  ngOnInit(): void {
  }

}
