import { SidebarMenuItem } from '../../_models/sidebar/sidebar-menu-item';
import { Pipe, PipeTransform } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import {Observable, of} from 'rxjs';
import {map, take} from 'rxjs/operators';

@Pipe({
  name: 'menuItemFilter'
})
export class MenuFilterPipe implements PipeTransform {

  constructor(private authService: AuthService){}

  transform(menuItems: SidebarMenuItem[], filter: string): Observable<SidebarMenuItem[]> {
    if (!menuItems || !filter) {
      return of(menuItems);
    }

    return this.authService.effectivePermissions$.pipe(take(1),
      map((permissions: number[]) => {
        return menuItems.filter(item =>
          item.section === filter && item.requiredPermissions.some(s => permissions.includes(s)));
    }));
  }
}
