import { Directive } from '@angular/core';
import {AppService} from '../../../_services/app.service';
import {InjectorService} from '../../../_services/injector.service';
import {AlertService} from '../../../_services/alert.service';
import {ActivatedRoute, Router} from '@angular/router';
import {AuthService} from '../../../_services/auth.service';

@Directive({
  selector: '[appBaseComponent]'
})
export abstract class BaseComponentDirective {

  protected appService: AppService;
  protected alertService: AlertService;
  protected router: Router;
  protected route: ActivatedRoute;
  protected authService: AuthService;
  componentName: string;

  protected constructor() {
    const injector = InjectorService.getInjector();
    this.appService = injector.get(AppService);
    this.alertService = injector.get(AlertService);
    this.router = injector.get(Router);
    this.route = injector.get(ActivatedRoute);
    this.authService = injector.get(AuthService);
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
}
