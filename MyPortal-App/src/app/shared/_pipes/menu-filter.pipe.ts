import { SidebarMenuItem } from '../../_models/sidebar/sidebar-menu-item';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'menuItemFilter'
})
export class MenuFilterPipe implements PipeTransform {

  transform(menuItems: SidebarMenuItem[], filter: string): SidebarMenuItem[] {
    if (!menuItems || !filter) {
      return menuItems;
    }

    // TODO: Add Permissions filtering
    const result = menuItems.filter(item => item.section === filter);
    return result;
  }

}
