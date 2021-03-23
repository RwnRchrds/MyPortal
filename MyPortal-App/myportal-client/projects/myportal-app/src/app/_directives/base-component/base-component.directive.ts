import {Directive, OnDestroy, OnInit} from '@angular/core';
import {AppService} from '../../_services/app.service';
import {InjectorService} from '../../_services/injector.service';
import {AlertService} from '../../_services/alert.service';
import {ActivatedRoute, Router} from '@angular/router';
import {AuthService} from '../../_services/auth.service';
import {HttpErrorResponse} from '@angular/common/http';
import {Observable, Subscription, throwError} from 'rxjs';
import {map, take} from 'rxjs/operators';

@Directive({
  selector: '[appBaseComponent]'
})
export abstract class BaseComponentDirective {

  protected userPermissions: number[];
  protected permissionSubscription: Subscription;
  protected appService: AppService;
  protected alertService: AlertService;
  protected router: Router;
  protected authService: AuthService;
  componentName: string;

  protected constructor() {
    const injector = InjectorService.getInjector();
    this.appService = injector.get(AppService);
    this.alertService = injector.get(AlertService);
    this.router = injector.get(Router);
    this.authService = injector.get(AuthService);
  }

  protected hasPermission(permissions: number[]): boolean {
    return permissions.some(p => this.userPermissions.includes(p));
  }

  protected populatePermissions(): void {
    this.permissionSubscription = this.authService.effectivePermissions$.pipe(take(1), map((permissions: number[]) => {
      this.userPermissions = permissions;
    })).subscribe();
  }

  protected get htmlComponentName(): string {
    return `#${this.componentName}`;
  }

  protected blockComponent(): void {
    this.appService.blockComponent(this.htmlComponentName);
  }

  protected unblockComponent(): void {
    this.appService.unblockComponent(this.htmlComponentName);
  }

  protected handleError(err: HttpErrorResponse): Observable<never> {
    this.alertService.error(err.error);
    return throwError(err);
  }
}
