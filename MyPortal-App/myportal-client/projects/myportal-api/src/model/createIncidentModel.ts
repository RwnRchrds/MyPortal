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

export interface CreateIncidentModel { 
    academicYearId?: string;
    behaviourTypeId: string;
    studentId?: string;
    locationId: string;
    outcomeId?: string;
    statusId: string;
    comments?: string;
    points: number;
}