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
import { AcademicYearModel } from './academicYearModel';
import { LogNoteTypeModel } from './logNoteTypeModel';
import { StudentModel } from './studentModel';
import { UserModel } from './userModel';

export interface LogNoteModel { 
    id?: string;
    typeId?: string;
    createdById?: string;
    updatedById?: string;
    studentId?: string;
    academicYearId?: string;
    message: string;
    createdDate?: Date;
    updatedDate?: Date;
    deleted?: boolean;
    createdBy?: UserModel;
    updatedBy?: UserModel;
    student?: StudentModel;
    academicYear?: AcademicYearModel;
    logNoteType?: LogNoteTypeModel;
}