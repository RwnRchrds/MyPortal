import { Component, OnInit } from '@angular/core';
import {CalendarEventModel, StudentModel} from 'myportal-api';
import {Observable, Subscription} from 'rxjs';
import {map} from 'rxjs/operators';
import {StudentViewService} from '../../student-view.service';
import {BaseCalendarDirective} from '../../../../../../_directives/base-calendar/base-calendar.directive';

@Component({
  selector: 'app-student-timetable',
  templateUrl: './student-timetable.component.html',
  styleUrls: ['./student-timetable.component.css']
})
export class StudentTimetableComponent extends BaseCalendarDirective implements OnInit {

  studentId: string;
  studentSubscription: Subscription;

  constructor(private viewService: StudentViewService) {
    super();
  }

  ngOnInit(): void {
    this.componentName = 'student_timetable';
    this.blockComponent();
    this.appService.blockPage();
    this.studentSubscription = this.viewService.currentStudent
      .pipe(map((student: StudentModel) => {
      if (student != null) {
        this.studentId = student.id;
      }
    })).subscribe();
  }

  protected getEvents(dateFrom: Date, dateTo: Date): Observable<CalendarEventModel[]> {
    return this.calendarService.getStudentCalendarEvents(this.studentId, dateFrom, dateTo);
  }
}
