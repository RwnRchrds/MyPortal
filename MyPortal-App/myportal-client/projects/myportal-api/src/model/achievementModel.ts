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
import { AcademicYearModel } from './academicYearModel';
import { AchievementOutcomeModel } from './achievementOutcomeModel';
import { AchievementTypeModel } from './achievementTypeModel';
import { LocationModel } from './locationModel';
import { StudentModel } from './studentModel';
import { UserModel } from './userModel';

export interface AchievementModel { 
    id?: string;
    academicYearId?: string;
    achievementTypeId: string;
    studentId?: string;
    locationId?: string;
    recordedById?: string;
    outcomeId?: string;
    createdDate?: Date;
    comments?: string;
    points: number;
    deleted?: boolean;
    type?: AchievementTypeModel;
    outcome?: AchievementOutcomeModel;
    location?: LocationModel;
    academicYear?: AcademicYearModel;
    recordedBy?: UserModel;
    student?: StudentModel;
}