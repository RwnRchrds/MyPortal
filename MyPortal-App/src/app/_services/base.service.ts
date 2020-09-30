import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  baseUrl: string;

constructor(protected http: HttpClient, serviceRoot: string) {
  this.baseUrl = `${environment.apiUrl}${serviceRoot}/`;
 }

}
