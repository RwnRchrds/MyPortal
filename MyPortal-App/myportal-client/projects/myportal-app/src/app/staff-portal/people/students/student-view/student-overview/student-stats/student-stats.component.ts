import { Component, OnInit } from '@angular/core';
import {StudentModel, StudentsService, StudentStatsModel} from 'myportal-api';
import {Subscription, throwError} from 'rxjs';
import {StudentViewService} from '../../student-view.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {BaseComponentDirective} from '../../../../../../_directives/base-component/base-component.directive';

@Component({
  selector: 'app-student-stats',
  templateUrl: './student-stats.component.html',
  styleUrls: ['./student-stats.component.css']
})
export class StudentStatsComponent extends BaseComponentDirective implements OnInit {

  statsModel: StudentStatsModel;
  private studentStatsSubscription: Subscription;

  constructor(private viewService: StudentViewService, private studentService: StudentsService) {
    super();
  }

  ngOnInit(): void {
    this.componentName = 'student_stats';
    this.appService.blockPage();
    this.studentStatsSubscription = this.viewService.currentStudent.pipe(map((student: StudentModel) => {
      if (student == null) {
        return;
      }
      this.studentService.getStatsById(student.id).pipe(map((stats: StudentStatsModel) => {
        this.statsModel = stats;
        this.appService.unblockPage();
      })).subscribe();
    }), catchError((err: HttpErrorResponse) => {
      this.appService.unblockPage();
      this.alertService.error(err.error);
      return throwError(err);
    })).subscribe();
  }

  get attendance(): string {
    if (this.statsModel != null && this.statsModel?.percentageAttendance != null) {
      return `${this.statsModel.percentageAttendance}%`;
    }
    else {
      return '--';
    }
  }

  get achievementPoints(): string {
    return `${this.statsModel?.achievementPoints}`;
  }

  get behaviourPoints(): string {
    return `${this.statsModel?.behaviourPoints}`;
  }

  get exclusions(): string {
    return `${this.statsModel?.exclusions}`;
  }

  reload(): void {
    if (this.statsModel == null) {
      return;
    }
    this.studentService.getStatsById(this.statsModel.studentId).pipe(map((statsModel: StudentStatsModel) => {
      this.statsModel = statsModel;
    })).subscribe();
  }

}
