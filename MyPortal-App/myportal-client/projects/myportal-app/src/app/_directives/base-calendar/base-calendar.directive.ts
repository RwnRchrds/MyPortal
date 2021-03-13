import { Directive } from '@angular/core';
import {InjectorService} from '../../_services/injector.service';
import {CalendarEventModel, CalendarService} from 'myportal-api';
import {CalendarOptions} from '@fullcalendar/angular';
import {Observable} from 'rxjs';
import {BaseComponentDirective} from '../base-component/base-component.directive';

@Directive({
  selector: '[appBaseCalendar]'
})
export abstract class BaseCalendarDirective extends BaseComponentDirective {

  protected calendarService: CalendarService;
  calendarOptions: CalendarOptions = {
    headerToolbar: {
      start: 'title',
      center: 'dayGridMonth timeGridWeek timeGridDay',
      end: 'today prev,next'
    },
    initialView: 'timeGridWeek',
    weekends: false,
    eventSources: [this.loadEvents(this)]
  };

  protected constructor() {
    super();
    const injector = InjectorService.getInjector();
    this.calendarService = injector.get(CalendarService);
  }

  protected loadEvents(arg): any {
    return (fetchInfo, successCallback, failureCallback) => {
      this.getEvents(fetchInfo.start, fetchInfo.end)
        .toPromise().then((events) => {
        successCallback(events);
      }).catch((error) => {
        failureCallback(error);
      });
    };
  }

  protected abstract getEvents(dateFrom: Date, dateTo: Date): Observable<CalendarEventModel[]>;
}

