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

import { CreateLogNoteModel } from '../model/createLogNoteModel';
import { LogNoteModel } from '../model/logNoteModel';
import { LogNoteTypeModel } from '../model/logNoteTypeModel';
import { UpdateLogNoteModel } from '../model/updateLogNoteModel';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class LogNotesService {

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
     * @param logNoteId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public _delete(logNoteId?: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public _delete(logNoteId?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public _delete(logNoteId?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public _delete(logNoteId?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (logNoteId !== undefined && logNoteId !== null) {
            queryParameters = queryParameters.set('logNoteId', <any>logNoteId);
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('delete',`${this.basePath}/api/student/logNotes/delete`,
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
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public create(body?: CreateLogNoteModel, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public create(body?: CreateLogNoteModel, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public create(body?: CreateLogNoteModel, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public create(body?: CreateLogNoteModel, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<any>('post',`${this.basePath}/api/student/logNotes/create`,
            {
                body: body,
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
     * @param logNoteId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getById(logNoteId?: string, observe?: 'body', reportProgress?: boolean): Observable<LogNoteModel>;
    public getById(logNoteId?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<LogNoteModel>>;
    public getById(logNoteId?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<LogNoteModel>>;
    public getById(logNoteId?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (logNoteId !== undefined && logNoteId !== null) {
            queryParameters = queryParameters.set('logNoteId', <any>logNoteId);
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

        return this.httpClient.request<LogNoteModel>('get',`${this.basePath}/api/student/logNotes/id`,
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
     * @param studentId 
     * @param academicYearId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getByStudent(studentId?: string, academicYearId?: string, observe?: 'body', reportProgress?: boolean): Observable<Array<LogNoteModel>>;
    public getByStudent(studentId?: string, academicYearId?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<LogNoteModel>>>;
    public getByStudent(studentId?: string, academicYearId?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<LogNoteModel>>>;
    public getByStudent(studentId?: string, academicYearId?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {



        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (studentId !== undefined && studentId !== null) {
            queryParameters = queryParameters.set('studentId', <any>studentId);
        }
        if (academicYearId !== undefined && academicYearId !== null) {
            queryParameters = queryParameters.set('academicYearId', <any>academicYearId);
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

        return this.httpClient.request<Array<LogNoteModel>>('get',`${this.basePath}/api/student/logNotes/student`,
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
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getTypes(observe?: 'body', reportProgress?: boolean): Observable<Array<LogNoteTypeModel>>;
    public getTypes(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<LogNoteTypeModel>>>;
    public getTypes(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<LogNoteTypeModel>>>;
    public getTypes(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

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

        return this.httpClient.request<Array<LogNoteTypeModel>>('get',`${this.basePath}/api/student/logNotes/types`,
            {
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
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public update(body?: UpdateLogNoteModel, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public update(body?: UpdateLogNoteModel, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public update(body?: UpdateLogNoteModel, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public update(body?: UpdateLogNoteModel, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {


        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<any>('put',`${this.basePath}/api/student/logNotes/update`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
