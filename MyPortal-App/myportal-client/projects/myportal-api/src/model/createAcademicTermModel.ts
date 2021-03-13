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
 */
import { CreateAttendanceWeekModel } from './createAttendanceWeekModel';
import { CreateTermWeekPatternModel } from './createTermWeekPatternModel';

export interface CreateAcademicTermModel { 
    name?: string;
    startDate?: Date;
    endDate?: Date;
    attendanceWeeks?: Array<CreateAttendanceWeekModel>;
    holidays?: Array<Date>;
    weekPatterns?: Array<CreateTermWeekPatternModel>;
}