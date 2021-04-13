import {Component, OnDestroy, OnInit} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {StudentViewService} from '../../../student-view.service';
import {LogNoteModel, LogNotesService, LogNoteTypeModel, StudentModel} from 'myportal-api';
import {catchError, map} from 'rxjs/operators';
import {Subscription, throwError} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';
import {BaseFormDirective} from '../../../../../../../_directives/base-form/base-form.directive';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-log-note-form',
  templateUrl: './log-note-form.component.html',
  styleUrls: ['./log-note-form.component.css']
})
export class LogNoteFormComponent extends BaseFormDirective implements OnInit, OnDestroy {

  studentId: string;
  studentSubscription: Subscription;

  logNoteId: string;
  editMode: boolean;
  logNoteTypes: LogNoteTypeModel[];

  get logNoteType(): AbstractControl {
    return this.form.get('logNoteType');
  }

  get message(): AbstractControl {
    return this.form.get('message');
  }

  constructor(private viewService: StudentViewService, private logNoteService: LogNotesService,
              private route: ActivatedRoute) {
    super();
  }

  ngOnInit(): void {
    this.componentName = 'logNote_form';
    this.form = new FormGroup({
      logNoteType: new FormControl('', [Validators.required]),
      message: new FormControl('', [Validators.required])
    });
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

  submit(): void {
    this.blockComponent();
    if (this.validate()) {
      if (this.editMode) {
        this.logNoteService.update({id: this.logNoteId, message: this.message.value, typeId: this.logNoteType.value}).pipe(map((result => {
          this.alertService.toastSuccess('Log note updated');
          this.unblockComponent();
          this.goBack();
        })), catchError((err: HttpErrorResponse) => {
          this.unblockComponent();
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      } else {
        this.logNoteService.create({
          studentId: this.studentId,
          message: this.message.value,
          typeId: this.logNoteType.value
        }).pipe(map((result => {
          this.alertService.toastSuccess('Log note created');
          this.unblockComponent();
          this.goBack();
        })), catchError((err: HttpErrorResponse) => {
          this.unblockComponent();
          this.alertService.error(err.error);
          return throwError(err);
        })).subscribe();
      }
    }
  }

  goBack(): void {
    this.router.navigate(['../../'], {relativeTo: this.route});
  }
}
