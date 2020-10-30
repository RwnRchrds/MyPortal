import {PortalSidebarComponent} from '../../shared/portal-sidebar/portal-sidebar.component';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-staff-sidebar',
  templateUrl: './staff-sidebar.component.html',
  styleUrls: ['./staff-sidebar.component.css']
})
export class StaffSidebarComponent extends PortalSidebarComponent implements OnInit {

  constructor() {
    super();

    this.homeRoute = '/staff/home';

    this.sidebarTitle = 'Staff Portal';

    this.menuItems = [
      {section: 'people', text: 'Students', route: '/staff/students'}
    ];
   }

  ngOnInit() {
  }
}
