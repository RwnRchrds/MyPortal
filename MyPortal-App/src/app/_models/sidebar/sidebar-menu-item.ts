export interface SidebarMenuItem {
    section: string;
    text: string;
    route: string;
    requiredPermission?: string[];
}
