import { SharedModule } from '../shared/shared.module';
import { StudentSearchComponent } from './people/students/student-browser/student-search/student-search.component';
import { StudentBrowserComponent } from './people/students/student-browser/student-browser.component';
import {Injector, NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { StaffRoutingModule } from './staff-routing/staff-routing.module';
import { StaffHomepageComponent } from './staff-homepage/staff-homepage.component';
import { StaffPortalComponent } from './staff-portal.component';
import { StaffSidebarComponent } from './staff-sidebar/staff-sidebar.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { RoleBrowserComponent } from './settings/roles/role-browser/role-browser.component';
import { RoleSearchComponent } from './settings/roles/role-browser/role-search/role-search.component';
import { RoleViewComponent } from './settings/roles/role-view/role-view.component';
import { RoleDetailsComponent } from './settings/roles/role-view/role-details/role-details.component';
import { CreateRoleComponent } from './settings/roles/role-browser/create-role/create-role.component';
import { UserBrowserComponent } from './settings/users/user-browser/user-browser.component';
import { UserSearchComponent } from './settings/users/user-browser/user-search/user-search.component';
import { UserViewComponent } from './settings/users/user-view/user-view.component';
import { UserDetailsComponent } from './settings/users/user-view/user-details/user-details.component';
import { UserLinkPersonComponent } from './settings/users/user-view/user-link-person/user-link-person.component';
import { UserResetPasswordComponent } from './settings/users/user-view/user-reset-password/user-reset-password.component';
import { CreateUserComponent } from './settings/users/user-browser/create-user/create-user.component';
import { StudentViewComponent } from './people/students/student-view/student-view.component';
import { StudentOverviewComponent } from './people/students/student-view/student-overview/student-overview.component';
import { StudentDetailsComponent } from './people/students/student-view/student-details/student-details.component';
import { StudentAssessmentComponent } from './people/students/student-view/student-assessment/student-assessment.component';
import { StudentAttendanceComponent } from './people/students/student-view/student-attendance/student-attendance.component';
import { StudentBehaviourComponent } from './people/students/student-view/student-behaviour/student-behaviour.component';
import { StudentCommunicationComponent } from './people/students/student-view/student-communication/student-communication.component';
import { StudentCurriculumComponent } from './people/students/student-view/student-curriculum/student-curriculum.component';
import { StudentDocumentsComponent } from './people/students/student-view/student-documents/student-documents.component';
import { StudentSendComponent } from './people/students/student-view/student-send/student-send.component';
import { StudentStatsComponent } from './people/students/student-view/student-overview/student-stats/student-stats.component';
import { StudentLogNotesComponent } from './people/students/student-view/student-overview/student-log-notes/student-log-notes.component';
import { LogNoteFormComponent } from './people/students/student-view/student-overview/student-log-notes/log-note-form/log-note-form.component';
import {InjectorService} from '../_services/injector.service';
import { StudentTimetableComponent } from './people/students/student-view/student-overview/student-timetable/student-timetable.component';
import {FullCalendarModule} from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import { StudentBasicDetailsComponent } from './people/students/student-view/student-overview/student-basic-details/student-basic-details.component';

FullCalendarModule.registerPlugins([
  dayGridPlugin,
  timeGridPlugin
]);

@NgModule({
  imports: [
    CommonModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    StaffRoutingModule,
    SharedModule,
    FullCalendarModule
  ],
  declarations: [
    StaffPortalComponent,
    StaffSidebarComponent,
    StaffHomepageComponent,
    StudentBrowserComponent,
    StudentSearchComponent,
    RoleBrowserComponent,
    RoleSearchComponent,
    RoleViewComponent,
    RoleDetailsComponent,
    CreateRoleComponent,
    UserBrowserComponent,
    UserSearchComponent,
    UserViewComponent,
    UserDetailsComponent,
    UserLinkPersonComponent,
    UserResetPasswordComponent,
    CreateUserComponent,
    StudentViewComponent,
    StudentOverviewComponent,
    StudentDetailsComponent,
    StudentAssessmentComponent,
    StudentAttendanceComponent,
    StudentBehaviourComponent,
    StudentCommunicationComponent,
    StudentCurriculumComponent,
    StudentDocumentsComponent,
    StudentSendComponent,
    StudentStatsComponent,
    StudentLogNotesComponent,
    LogNoteFormComponent,
    StudentTimetableComponent,
    StudentBasicDetailsComponent
  ],
  exports: [
    StaffPortalComponent,
    StaffSidebarComponent
  ],
  providers: []
})
export class StaffPortalModule {
  constructor(injector: Injector) {
    InjectorService.setInjector(injector);
  }
}
