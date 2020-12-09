/**
 * MyPortal
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 *//* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { StudentDataGridModel } from '../model/studentDataGridModel';
import { StudentModel } from '../model/studentModel';
import { StudentStatus } from '../model/studentStatus';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class StudentsService {

    protected basePath = '/';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * 
     * 
     * @param studentId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getById(studentId?: string, observe?: 'body', reportProgress?: boolean): Observable<Array<StudentModel>>;
    public getById(studentId?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<StudentModel>>>;
    public getById(studentId?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<StudentModel>>>;
    public getById(studentId?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (studentId !== undefined && studentId !== null) {
            queryParameters = queryParameters.set('studentId', <any>studentId);
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<Array<StudentModel>>('get',`${this.basePath}/api/students/id`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param status 
     * @param curriculumGroupId 
     * @param regGroupId 
     * @param yearGroupId 
     * @param houseId 
     * @param senStatusId 
     * @param firstName 
     * @param lastName 
     * @param gender 
     * @param dob 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public searchStudents(status?: StudentStatus, curriculumGroupId?: string, regGroupId?: string, yearGroupId?: string, houseId?: string, senStatusId?: string, firstName?: string, lastName?: string, gender?: string, dob?: Date, observe?: 'body', reportProgress?: boolean): Observable<Array<StudentDataGridModel>>;
    public searchStudents(status?: StudentStatus, curriculumGroupId?: string, regGroupId?: string, yearGroupId?: string, houseId?: string, senStatusId?: string, firstName?: string, lastName?: string, gender?: string, dob?: Date, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<StudentDataGridModel>>>;
    public searchStudents(status?: StudentStatus, curriculumGroupId?: string, regGroupId?: string, yearGroupId?: string, houseId?: string, senStatusId?: string, firstName?: string, lastName?: string, gender?: string, dob?: Date, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<StudentDataGridModel>>>;
    public searchStudents(status?: StudentStatus, curriculumGroupId?: string, regGroupId?: string, yearGroupId?: string, houseId?: string, senStatusId?: string, firstName?: string, lastName?: string, gender?: string, dob?: Date, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {











        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (status !== undefined && status !== null) {
            queryParameters = queryParameters.set('Status', <any>status);
        }
        if (curriculumGroupId !== undefined && curriculumGroupId !== null) {
            queryParameters = queryParameters.set('CurriculumGroupId', <any>curriculumGroupId);
        }
        if (regGroupId !== undefined && regGroupId !== null) {
            queryParameters = queryParameters.set('RegGroupId', <any>regGroupId);
        }
        if (yearGroupId !== undefined && yearGroupId !== null) {
            queryParameters = queryParameters.set('YearGroupId', <any>yearGroupId);
        }
        if (houseId !== undefined && houseId !== null) {
            queryParameters = queryParameters.set('HouseId', <any>houseId);
        }
        if (senStatusId !== undefined && senStatusId !== null) {
            queryParameters = queryParameters.set('SenStatusId', <any>senStatusId);
        }
        if (firstName !== undefined && firstName !== null) {
            queryParameters = queryParameters.set('FirstName', <any>firstName);
        }
        if (lastName !== undefined && lastName !== null) {
            queryParameters = queryParameters.set('LastName', <any>lastName);
        }
        if (gender !== undefined && gender !== null) {
            queryParameters = queryParameters.set('Gender', <any>gender);
        }
        if (dob !== undefined && dob !== null) {
            queryParameters = queryParameters.set('Dob', <any>dob.toISOString());
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<Array<StudentDataGridModel>>('get',`${this.basePath}/api/students/search`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
