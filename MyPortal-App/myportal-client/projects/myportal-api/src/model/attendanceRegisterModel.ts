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
 */
import { AttendanceCodeModel } from './attendanceCodeModel';
import { AttendanceRegisterStudentModel } from './attendanceRegisterStudentModel';
import { SessionMetadata } from './sessionMetadata';

export interface AttendanceRegisterModel { 
    metadata?: SessionMetadata;
    codes?: Array<AttendanceCodeModel>;
    students?: Array<AttendanceRegisterStudentModel>;
}