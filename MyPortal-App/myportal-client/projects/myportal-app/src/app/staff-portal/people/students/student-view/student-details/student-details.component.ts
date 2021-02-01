import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {StudentViewService} from '../student-view.service';

@Component({
  selector: 'app-student-details',
  templateUrl: './student-details.component.html',
  styleUrls: ['./student-details.component.css']
})
export class StudentDetailsComponent implements OnInit {

  constructor(private viewService: StudentViewService, private cdRef: ChangeDetectorRef) { }

  ngOnInit(): void {
  }

}
