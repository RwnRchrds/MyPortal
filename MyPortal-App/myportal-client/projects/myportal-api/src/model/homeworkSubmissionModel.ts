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
import { DocumentModel } from './documentModel';
import { HomeworkModel } from './homeworkModel';
import { StudentModel } from './studentModel';
import { TaskModel } from './taskModel';

export interface HomeworkSubmissionModel { 
    id?: string;
    homeworkId?: string;
    studentId?: string;
    taskId?: string;
    documentId?: string;
    maxPoints?: number;
    pointsAchieved?: number;
    comments?: string;
    homeworkItem?: HomeworkModel;
    student?: StudentModel;
    task?: TaskModel;
    submittedWork?: DocumentModel;
}