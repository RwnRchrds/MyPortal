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
import { SubjectModel } from './subjectModel';

export interface CourseModel { 
    id?: string;
    description: string;
    active?: boolean;
    subjectId?: string;
    name?: string;
    subject?: SubjectModel;
}