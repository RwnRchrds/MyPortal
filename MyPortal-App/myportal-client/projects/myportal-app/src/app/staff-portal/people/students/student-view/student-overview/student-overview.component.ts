import {ChangeDetectorRef, Component, OnDestroy, OnInit} from '@angular/core';
import {StudentViewService} from '../student-view.service';

@Component({
  selector: 'app-student-overview',
  templateUrl: './student-overview.component.html',
  styleUrls: ['./student-overview.component.css']
})
export class StudentOverviewComponent implements OnInit {

  constructor(private viewService: StudentViewService, private cdRef: ChangeDetectorRef) { }

  ngOnInit(): void {
  }
}
