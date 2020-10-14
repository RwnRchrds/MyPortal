import { StudentSearch } from '../_models/People/Students/student-search';
import {HttpClient, HttpParams} from '@angular/common/http';
import { BaseService } from './base.service';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StudentService extends BaseService {

constructor(http: HttpClient) {
  super(http, 'student');
}

search(searchModel: StudentSearch): any {
  const searchParams = new HttpParams();
  searchParams.append('firstName', searchModel.firstName);
  searchParams.append('lastName', searchModel.lastName);
  searchParams.append('gender', searchModel.gender);
  searchParams.append('dob', searchModel.dob?.toDateString());
  searchParams.append('status', searchModel.status);
  searchParams.append('curriculumGroupId', searchModel.curriculumGroupId);
  searchParams.append('regGroupId', searchModel.regGroupId);
  searchParams.append('yearGroupId', searchModel.yearGroupId);
  searchParams.append('houseId', searchModel.houseId);
  searchParams.append('senStatusId', searchModel.senStatusId);
  return this.http.get(this.baseUrl + 'search', {params: searchParams}).pipe(map(response => {
    console.log(response);
  }));
}

}
