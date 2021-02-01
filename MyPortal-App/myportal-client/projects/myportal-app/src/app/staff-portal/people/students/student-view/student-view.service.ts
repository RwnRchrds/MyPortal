import { Injectable } from '@angular/core';
import {BehaviorSubject, throwError} from 'rxjs';
import {StudentModel, StudentsService} from 'myportal-api';
import {catchError, map, take} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {AlertService} from '../../../../_services/alert.service';

@Injectable({
  providedIn: 'root'
})
export class StudentViewService {

  private studentSource: BehaviorSubject<StudentModel> = new BehaviorSubject<StudentModel>(null);
  currentStudent = this.studentSource.asObservable();

  init(studentId: string): void {
    this.studentService.getById(studentId).pipe(map((student: StudentModel) => {
      this.studentSource.next(student);
    }), catchError((err: HttpErrorResponse) => {
      this.alertService.error(err.error);
      return throwError(err);
    })).subscribe();
  }

  reset(): void {
    this.studentSource.next(null);
  }

  reload(): void {
    this.currentStudent.pipe(take(1), map((student: StudentModel) => {
      if (student == null) {
        return;
      }
      this.init(student.id);
    })).subscribe();
  }


  constructor(private studentService: StudentsService, private alertService: AlertService) { }
}
