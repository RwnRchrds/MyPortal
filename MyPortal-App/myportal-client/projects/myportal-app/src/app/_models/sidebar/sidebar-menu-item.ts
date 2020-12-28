export interface SidebarMenuItem {
    section: string;
    label: string;
    route: string;
    requiredPermissions?: string[];
}
