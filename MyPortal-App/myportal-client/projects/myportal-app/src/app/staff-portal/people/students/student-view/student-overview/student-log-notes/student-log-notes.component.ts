import {Component, OnDestroy, OnInit} from '@angular/core';
import {LogNoteModel, LogNotesService, StudentModel} from 'myportal-api';
import {Subscription, throwError} from 'rxjs';
import {StudentViewService} from '../../student-view.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {AlertService} from '../../../../../../_services/alert.service';
import {AppService} from '../../../../../../_services/app.service';
import {BaseComponentDirective} from '../../../../../../shared/base-component.directive';
import {ActivatedRoute, Router} from '@angular/router';
import {AuthService} from '../../../../../../_services/auth.service';
import {AppPermissions} from '../../../../../../_constants/app-permissions';

@Component({
  selector: 'app-student-log-notes',
  templateUrl: './student-log-notes.component.html',
  styleUrls: ['./student-log-notes.component.css']
})
export class StudentLogNotesComponent extends BaseComponentDirective implements OnInit, OnDestroy {

  studentId: string;
  logNotes: LogNoteModel[];
  studentSubscription: Subscription;

  constructor(private viewService: StudentViewService, private logNoteService: LogNotesService,
              private alertService: AlertService, protected appService: AppService,
              private router: Router, private route: ActivatedRoute, private authService: AuthService) {
    super(appService);
  }

  ngOnInit(): void {
    this.componentName = '#student_logNotes';
    this.appService.blockPage();
    this.studentSubscription = this.viewService.currentStudent.pipe(map((student: StudentModel) => {
      if (student != null) {
        this.studentId = student.id;
        this.logNoteService.getByStudent(student.id).pipe(map((logNotes: LogNoteModel[]) => {
          this.logNotes = logNotes;
          this.appService.unblockPage();
        }), catchError((err: HttpErrorResponse) => {
          this.appService.unblockPage();
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      }
    })).subscribe();
  }

  ngOnDestroy(): void {
    this.studentId = null;
    this.studentSubscription.unsubscribe();
  }

  get allowEdit(): boolean {
    return this.authService.hasPermission([AppPermissions.STUDENTS_LOGNOTES_EDIT]);
  }

  newLogNote(): void {
    this.router.navigate(['logNotes/new'], {relativeTo: this.route});
  }

  editLogNote(logNoteId: string): void {
    this.router.navigate([`logNotes/${logNoteId}`], {relativeTo: this.route});
  }

  delete(logNoteId: string): void {
    this.alertService.areYouSure(`Are you sure you want to delete this log note?`).then(userResponse => {
      if (userResponse.isConfirmed) {
        this.appService.blockPage();
        this.logNoteService._delete(logNoteId).pipe(map(result => {
          this.alertService.toastSuccess('Log note deleted');
          this.reload();
          this.appService.unblockPage();
        }), catchError((err: HttpErrorResponse) => {
          this.appService.unblockPage();
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      }
    });
  }

  reload(): void {
    if (this.studentId == null) {
      return;
    }
    this.blockComponent();
    this.logNoteService.getByStudent(this.studentId).pipe(map((logNotes: LogNoteModel[]) => {
      this.logNotes = logNotes;
      this.unblockComponent();
    }), catchError((err: HttpErrorResponse) => {
      this.unblockComponent();
      this.alertService.error(err.error);
      return throwError(err);
    })).subscribe();
  }
}
