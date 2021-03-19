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

export interface SessionMetadata { 
    sessionId?: string;
    attendanceWeekId?: string;
    periodId?: string;
    classId?: string;
    curriculumGroupId?: string;
    startTime?: Date;
    endTime?: Date;
    periodName?: string;
    classCode?: string;
    courseDescription?: string;
    teacherId?: string;
    teacherName?: string;
    roomId?: string;
    roomName?: string;
    isCover?: boolean;
}