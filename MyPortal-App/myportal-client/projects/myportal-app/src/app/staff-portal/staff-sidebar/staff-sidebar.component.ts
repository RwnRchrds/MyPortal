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
      // {section: 'assessment', text: 'Aspects', route: ''},
      // {section: 'assessment', text: 'Examinations', route: ''},
      // {section: 'assessment', text: 'Grade Sets', route: ''},
      // {section: 'assessment', text: 'Marksheets', route: ''},
      // {section: 'assessment', text: 'Marksheet Templates', route: ''},
      // {section: 'assessment', text: 'Result Sets', route: ''},
      // {section: 'attendance', text: 'Edit Marks', route: ''},
      // {section: 'attendance', text: 'Take Register', route: ''},
      // {section: 'behaviour', text: 'Detentions', route: ''},
      // {section: 'calendar', text: 'School Diary', route: ''},
      // {section: 'communication', text: 'Emails', route: ''},
      // {section: 'curriculum', text: 'Lesson Plans', route: ''},
      // {section: 'documents', text: 'School Documents', route: ''},
      // {section: 'finance', text: 'Accounts', route: ''},
      // {section: 'people', text: 'Agents', route: ''},
      // {section: 'people', text: 'Contacts', route: ''},
      // {section: 'people', text: 'Staff', route: ''},
       {section: 'people', text: 'Students', route: '/staff/students'},
      // {section: 'personnel', text: 'Training Courses', route: ''},
      // {section: 'reports', text: 'Run Report', route: ''},
      // {section: 'school', text: 'School Details', route: ''},
       {section: 'settings', text: 'Users', route: ''}
    ];
   }

  ngOnInit(): void {
  }
}
