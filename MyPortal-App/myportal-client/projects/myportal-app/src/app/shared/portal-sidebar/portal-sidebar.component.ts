import { SidebarMenuItem } from '../../_models/sidebar/sidebar-menu-item';

export abstract class PortalSidebarComponent {

  menuItems: SidebarMenuItem[];

  homeRoute: string;

  sidebarTitle: string;

  constructor() { }

}
