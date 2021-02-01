import { Component, OnInit } from '@angular/core';
import {LogNoteModel, LogNotesService, StudentModel} from 'myportal-api';
import {Subscription, throwError} from 'rxjs';
import {StudentViewService} from '../../student-view.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {AlertService} from '../../../../../../_services/alert.service';

@Component({
  selector: 'app-student-log-notes',
  templateUrl: './student-log-notes.component.html',
  styleUrls: ['./student-log-notes.component.css']
})
export class StudentLogNotesComponent implements OnInit {

  logNotes: LogNoteModel[];
  studentSubscription: Subscription;

  constructor(private viewService: StudentViewService, private logNoteService: LogNotesService,
              private alertService: AlertService) { }

  ngOnInit(): void {
    this.studentSubscription = this.viewService.currentStudent.pipe(map((student: StudentModel) => {
      if (student != null) {
        this.logNoteService.getByStudent(student.id).pipe(map((logNotes: LogNoteModel[]) => {
          this.logNotes = logNotes;
        }), catchError((err: HttpErrorResponse) => {
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      }
    })).subscribe();
  }

}
