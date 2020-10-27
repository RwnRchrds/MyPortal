import {PortalSidebarComponent} from '../../shared/portal-sidebar/portal-sidebar.component';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-staff-sidebar',
  templateUrl: './staff-sidebar.component.html',
  styleUrls: ['./staff-sidebar.component.css']
})
export class StaffSidebarComponent extends PortalSidebarComponent implements OnInit {

  assessmentMenu = 'assessment';
  attendanceMenu = 'attendance';
  behaviourMenu = 'behaviour';
  calendarMenu = 'calendar';
  communicationMenu = 'communication';
  curriculumMenu = 'curriculum';
  documentsMenu = 'documents';
  financeMenu = 'finance';
  peopleMenu = 'people';
  personnelMenu = 'personnel';
  reportsMenu = 'reports';
  schoolMenu = 'school';
  settingsMenu = 'settings';

  constructor() {
    super();

    this.homeRoute = '/staff/home';

    this.sidebarTitle = 'Staff Portal';

    this.menuItems = [
      {parentId: 'people', text: 'Students', route: '/staff/students'}
    ];
   }

  ngOnInit() {
  }
}
