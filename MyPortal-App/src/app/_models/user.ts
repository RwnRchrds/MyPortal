export interface User {
    displayName: string;
    userType: string;
    permissions: string[];
    token: string;
    refreshToken: string;
}
