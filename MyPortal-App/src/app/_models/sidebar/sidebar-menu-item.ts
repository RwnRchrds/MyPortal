export interface SidebarMenuItem {
    parentId: string;
    text: string;
    route: string;
    requiredPermission?: string[];
}
