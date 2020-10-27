import { StudentDataGridModel } from './../_models/People/Students/student-datagrid';
import { StudentSearch } from '../_models/People/Students/student-search';
import {HttpClient, HttpParams} from '@angular/common/http';
import { BaseService } from './base.service';
import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StudentService extends BaseService {

constructor(http: HttpClient) {
  super(http, 'student');
}

search(searchModel: StudentSearch): Observable<StudentDataGridModel[]> {
  let searchParams: HttpParams = new HttpParams();

  if (!!searchModel.firstName)
  {
    searchParams = searchParams.set('firstName', searchModel.firstName);
  }

  if (!!searchModel.lastName)
  {
    searchParams = searchParams.set('lastName', searchModel.lastName);
  }

  if (!!searchModel.gender)
  {
    searchParams = searchParams.set('gender', searchModel.gender);
  }

  if (!!searchModel.status)
  {
    searchParams = searchParams.set('status', searchModel.status);
  }

  if (!!searchModel.curriculumGroupId)
  {
    searchParams = searchParams.set('curriculumGroupId', searchModel.curriculumGroupId);
  }

  if (!!searchModel.regGroupId)
  {
    searchParams = searchParams.set('regGroupId', searchModel.regGroupId);
  }

  if (!!searchModel.yearGroupId)
  {
    searchParams = searchParams.set('yearGroupId', searchModel.yearGroupId);
  }

  if (!!searchModel.houseId)
  {
    searchParams = searchParams.set('houseId', searchModel.houseId);
  }

  if (!!searchModel.senStatusId)
  {
    searchParams = searchParams.set('senStatusId', searchModel.senStatusId);
  }

  return this.http.get(this.baseUrl + 'search', {params: searchParams}).pipe(map((response: StudentDataGridModel[]) => {
    return response;
  }));
}

}
