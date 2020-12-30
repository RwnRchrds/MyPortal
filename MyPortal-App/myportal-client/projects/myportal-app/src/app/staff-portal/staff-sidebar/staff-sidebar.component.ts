import {PortalSidebarComponent} from '../../shared/portal-sidebar/portal-sidebar.component';
import { Component, OnInit } from '@angular/core';
import { AppPermissions } from '../../_guards/app-permissions';

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
      // {section: 'admissions', label: 'Applications', route: ''},
      // {section: 'admissions', label: 'Enquiries', route: ''},
      // {section: 'assessment', label: 'Aspects', route: ''},
      // {section: 'assessment', label: 'Examinations', route: ''},
      // {section: 'assessment', label: 'Grade Sets', route: ''},
      // {section: 'assessment', label: 'Marksheets', route: ''},
      // {section: 'assessment', label: 'Marksheet Templates', route: ''},
      // {section: 'assessment', label: 'Result Sets', route: ''},
      // {section: 'attendance', label: 'Edit Marks', route: ''},
      // {section: 'attendance', label: 'Take Register', route: ''},
      // {section: 'attendance', label: 'Settings', route: ''},
      // {section: 'behaviour', label: 'Detentions', route: ''},
      // {section: 'behaviour', label: 'Settings', route: ''},
      // {section: 'calendar', label: 'School Diary', route: ''},
      // {section: 'communication', label: 'Emails', route: ''},
      // {section: 'curriculum', label: 'Curriculum Structure', route: ''},
      // {section: 'curriculum', label: 'Lesson Plans', route: ''},
      // {section: 'curriculum', label: 'Homework', route: ''},
      // {section: 'curriculum', label: 'Study Topics', route: ''},
      // {section: 'documents', label: 'School Documents', route: ''},
      // {section: 'finance', label: 'Accounts', route: ''},
      // {section: 'finance', label: 'Settings', route: ''},
      // {section: 'people', label: 'Agents', route: ''},
      // {section: 'people', label: 'Contacts', route: ''},
      // {section: 'people', label: 'Staff', route: ''},
      {section: 'people', label: 'Students', route: '/staff/students', requiredPermissions: [
        AppPermissions.students_studentDetails_view
      ]},
      // {section: 'personnel', label: 'Training Courses', route: ''},
      // {section: 'reports', label: 'Run Report', route: ''},
      // {section: 'school', label: 'School Details', route: ''},
      // {section: 'settings', label: 'Users', route: '/settings/users', requiredPermission: [
      //   AppPermissions.system_users_view
      // ]},
      {section: 'settings', label: 'Roles', route: '/staff/settings/roles', requiredPermissions: [
        AppPermissions.system_users_view
      ]}
    ];
   }

  ngOnInit(): void {
  }
}
