import {Component, OnDestroy, OnInit} from '@angular/core';
import {BaseComponentDirective} from '../../../../../../_directives/base-component/base-component.directive';
import {AddressModel, AddressService, GiftedTalentedModel, SenService, StudentModel} from 'myportal-api';
import {Subscription, throwError} from 'rxjs';
import {StudentViewService} from '../../student-view.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-student-basic-details',
  templateUrl: './student-basic-details.component.html',
  styleUrls: ['./student-basic-details.component.css']
})
export class StudentBasicDetailsComponent extends BaseComponentDirective implements OnInit, OnDestroy {

  student: StudentModel;
  addresses: AddressModel[];
  giftedTalented: GiftedTalentedModel[];
  studentSubscription: Subscription;

  constructor(private viewService: StudentViewService, private addressService: AddressService,
              private senService: SenService) {
    super();
  }

  get firstAddress(): AddressModel {
    if (this.addresses.length > 0) {
      return this.addresses[0];
    }
    return null;
  }

  get isGiftedTalented(): boolean {
    return this.giftedTalented.length > 0;
  }

  ngOnInit(): void {
    this.componentName = 'student_basic_details';
    this.blockComponent();
    this.studentSubscription = this.viewService.currentStudent.pipe(
      map((student: StudentModel) => {
      this.student = student;
      this.addressService.getAddressesByPerson(student.personId).pipe(
        map((addresses: AddressModel[]) => {
        this.addresses = addresses;
      }), catchError((err: HttpErrorResponse) => {
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
      this.senService.getGiftedTalentedByStudent(student.id).pipe(
        map((giftedTalented: GiftedTalentedModel[]) => {
        this.giftedTalented = giftedTalented;
      }), catchError((err: HttpErrorResponse) => {
        this.alertService.error(err.error);
        return throwError(err);
      })).subscribe();
    })).subscribe();
  }

  ngOnDestroy(): void {
    this.studentSubscription.unsubscribe();
  }
}
