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

export interface TaskTypeModel { 
    id?: string;
    description: string;
    active?: boolean;
    personal?: boolean;
    colourCode: string;
    system?: boolean;
    reserved?: boolean;
}