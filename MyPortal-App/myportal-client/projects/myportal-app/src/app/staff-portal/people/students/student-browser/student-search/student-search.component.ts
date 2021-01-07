import { Component, OnInit } from '@angular/core';
import {StudentSearchService} from './student-search.service';

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html',
  styleUrls: ['./student-search.component.css']
})
export class StudentSearchComponent implements OnInit {

  service: StudentSearchService;

  constructor(studentSearchService: StudentSearchService) {
    this.service = studentSearchService;
   }

  ngOnInit(): void {
    this.service.loadSearchModel();
  }


}
