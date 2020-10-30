export interface StudentEntity {
    id: string;
    personId: string;
    yearGroupId: string;
    houseId: string;
    admissionNumber: number;
    dateStarting: Date;
    dateLeaving: Date;
    accountBalance: number;
    freeSchoolMeals: boolean;
    senStatusId: string;
    pupilPremium: boolean;
    upn: string;
    deleted: boolean;
}
