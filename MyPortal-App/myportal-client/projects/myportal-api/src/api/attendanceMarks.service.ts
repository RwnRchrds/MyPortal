/**
 * MyPortal
 * MyPortal Master Web Service
 *
 * OpenAPI spec version: 3.1.0
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

import { AttendanceRegisterModel } from '../model/attendanceRegisterModel';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class AttendanceMarksService {

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
     * @param attendanceWeekId 
     * @param sessionId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getRegister(attendanceWeekId: string, sessionId: string, observe?: 'body', reportProgress?: boolean): Observable<AttendanceRegisterModel>;
    public getRegister(attendanceWeekId: string, sessionId: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<AttendanceRegisterModel>>;
    public getRegister(attendanceWeekId: string, sessionId: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<AttendanceRegisterModel>>;
    public getRegister(attendanceWeekId: string, sessionId: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (attendanceWeekId === null || attendanceWeekId === undefined) {
            throw new Error('Required parameter attendanceWeekId was null or undefined when calling getRegister.');
        }

        if (sessionId === null || sessionId === undefined) {
            throw new Error('Required parameter sessionId was null or undefined when calling getRegister.');
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

        return this.httpClient.request<AttendanceRegisterModel>('get',`${this.basePath}/api/attendanceMarks/register/${encodeURIComponent(String(attendanceWeekId))}/${encodeURIComponent(String(sessionId))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
