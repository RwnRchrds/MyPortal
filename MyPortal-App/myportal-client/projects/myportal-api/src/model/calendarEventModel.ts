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
import { CalendarEventExtendedPropertiesModel } from './calendarEventExtendedPropertiesModel';

export interface CalendarEventModel { 
    id?: string;
    allDay?: boolean;
    start?: Date;
    end?: Date;
    title?: string;
    url?: string;
    classNames?: Array<string>;
    editable?: boolean;
    display?: string;
    color?: string;
    extendedProps?: CalendarEventExtendedPropertiesModel;
}