import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-student-search',
  templateUrl: './student-search.component.html',
  styleUrls: ['./student-search.component.scss']
})
export class StudentSearchComponent implements OnInit {

  firstName: string;
  lastName: string;
  

  constructor() { }

  ngOnInit() {
  }

}
