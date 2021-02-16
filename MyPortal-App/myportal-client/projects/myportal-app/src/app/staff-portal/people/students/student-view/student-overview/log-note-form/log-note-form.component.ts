import {Component, OnDestroy, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {StudentViewService} from '../../student-view.service';
import {LogNoteModel, LogNotesService, LogNoteTypeModel, StudentModel} from 'myportal-api';
import {ActivatedRoute, Route, Router} from '@angular/router';
import {catchError, map} from 'rxjs/operators';
import {Subscription, throwError} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';
import {AlertService} from '../../../../../../_services/alert.service';
import {AppService} from '../../../../../../_services/app.service';

@Component({
  selector: 'app-log-note-form',
  templateUrl: './log-note-form.component.html',
  styleUrls: ['./log-note-form.component.css']
})
export class LogNoteFormComponent implements OnInit, OnDestroy {

  studentId: string;
  studentSubscription: Subscription;

  logNoteId: string;
  editMode: boolean;
  logNoteTypes: LogNoteTypeModel[];

  logNoteForm = new FormGroup({
    logNoteType: new FormControl('', [Validators.required]),
    message: new FormControl('', [Validators.required])
  });

  get logNoteType(): AbstractControl {
    return this.logNoteForm.get('logNoteType');
  }

  get message(): AbstractControl {
    return this.logNoteForm.get('message');
  }

  constructor(private viewService: StudentViewService, private logNoteService: LogNotesService,
              private route: ActivatedRoute, private router: Router, private alertService: AlertService,
              private appService: AppService) { }

  ngOnInit(): void {
    console.log(this.route);
    this.appService.blockPage();
    this.logNoteService.getTypes().pipe(map((logNoteTypes: LogNoteTypeModel[]) => {
      this.logNoteTypes = logNoteTypes;
      this.appService.unblockPage();
    }), catchError((err: HttpErrorResponse) => {
      this.appService.unblockPage();
      this.alertService.error(err.error);
      return throwError(err);
    })).subscribe();

    this.studentSubscription = this.viewService.currentStudent.pipe(map((student: StudentModel) => {
      this.studentId = student.id;
    })).subscribe();
    this.logNoteId = this.route.snapshot.paramMap.get('logNoteId');
    this.editMode = !!this.logNoteId;

    if (this.editMode) {
      console.log(this.logNoteId);
      this.appService.blockPage();
      this.logNoteService.getById(this.logNoteId).pipe(map((logNote: LogNoteModel) => {
        this.logNoteType.setValue(logNote.logNoteType.id);
        this.message.setValue(logNote.message);
        this.appService.unblockPage();
      }), catchError((err: HttpErrorResponse) => {
        this.appService.unblockPage();
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
    }
  }

  ngOnDestroy(): void {
    this.studentSubscription.unsubscribe();
  }

  save(): void {
    if (this.editMode) {
      this.appService.blockPage();
      this.logNoteService.update({id: this.logNoteId, message: this.message.value, typeId: this.logNoteType.value}).pipe(map((result => {
        this.alertService.toastSuccess('Log note updated');
        this.appService.unblockPage();
        this.goBack();
      })), catchError((err: HttpErrorResponse) => {
        this.appService.unblockPage();
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
    } else {
      this.appService.blockPage();
      this.logNoteService.create({
        studentId: this.studentId,
        message: this.message.value,
        typeId: this.logNoteType.value
      }).pipe(map((result => {
        this.alertService.toastSuccess('Log note created');
        this.appService.unblockPage();
        this.goBack();
      })), catchError((err: HttpErrorResponse) => {
        this.appService.unblockPage();
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
    }
  }

  goBack(): void {
    this.router.navigate(['../../'], {relativeTo: this.route});
  }
}
