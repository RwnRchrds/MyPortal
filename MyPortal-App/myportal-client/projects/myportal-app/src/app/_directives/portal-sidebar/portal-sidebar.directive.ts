import { SidebarMenuItem } from '../../../_models/sidebar/sidebar-menu-item';

export abstract class PortalSidebarDirective {

  menuItems: SidebarMenuItem[];

  homeRoute: string;

  sidebarTitle: string;

  constructor() { }

}
