import { TokenWrapper } from './token-wrapper';
export interface User extends TokenWrapper {
    displayName: string;
    permissions: string[];
    userType: string;
}
