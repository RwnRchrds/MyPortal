import { TokenModel } from 'myportal-api';
export interface User extends TokenModel {
    displayName: string;
    permissions: string[];
    userType: string;
}
