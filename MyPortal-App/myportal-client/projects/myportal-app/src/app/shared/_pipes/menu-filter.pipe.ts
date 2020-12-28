import { SidebarMenuItem } from '../../_models/sidebar/sidebar-menu-item';
import { Pipe, PipeTransform } from '@angular/core';
import { AuthService } from '../../_services/auth.service';

@Pipe({
  name: 'menuItemFilter'
})
export class MenuFilterPipe implements PipeTransform {

  constructor(private authService: AuthService){}

  transform(menuItems: SidebarMenuItem[], filter: string): SidebarMenuItem[] {
    if (!menuItems || !filter) {
      return menuItems;
    }

    const result = menuItems.filter(item => item.section === filter && this.authService.hasPermission(item.requiredPermissions));
    return result;
  }
}
