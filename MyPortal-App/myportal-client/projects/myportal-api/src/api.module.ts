import { NgModule, ModuleWithProviders, SkipSelf, Optional } from '@angular/core';
import { Configuration } from './configuration';
import { HttpClient } from '@angular/common/http';


import { AchievementsService } from './api/achievements.service';
import { AuthenticationService } from './api/authentication.service';
import { BillsService } from './api/bills.service';
import { DirectoriesService } from './api/directories.service';
import { DocumentsService } from './api/documents.service';
import { HousesService } from './api/houses.service';
import { IncidentsService } from './api/incidents.service';
import { LogNotesService } from './api/logNotes.service';
import { RegGroupsService } from './api/regGroups.service';
import { RolesService } from './api/roles.service';
import { SchoolsService } from './api/schools.service';
import { StudentsService } from './api/students.service';
import { TasksService } from './api/tasks.service';
import { UsersService } from './api/users.service';
import { YearGroupsService } from './api/yearGroups.service';

@NgModule({
  imports:      [],
  declarations: [],
  exports:      [],
  providers: [
    AchievementsService,
    AuthenticationService,
    BillsService,
    DirectoriesService,
    DocumentsService,
    HousesService,
    IncidentsService,
    LogNotesService,
    RegGroupsService,
    RolesService,
    SchoolsService,
    StudentsService,
    TasksService,
    UsersService,
    YearGroupsService ]
})
export class ApiModule {
    public static forRoot(configurationFactory: () => Configuration): ModuleWithProviders<ApiModule> {
        return {
            ngModule: ApiModule,
            providers: [ { provide: Configuration, useFactory: configurationFactory } ]
        };
    }

    constructor( @Optional() @SkipSelf() parentModule: ApiModule,
                 @Optional() http: HttpClient) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
            'See also https://github.com/angular/angular/issues/20575');
        }
    }
}
