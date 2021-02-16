import { Directive } from '@angular/core';
import {AppService} from '../_services/app.service';

@Directive({
  selector: '[appBaseComponent]'
})
export class BaseComponentDirective {

  componentName: string;

  constructor(protected appService: AppService) { }

  blockComponent(): void {
    this.appService.blockComponent(this.componentName);
  }

  unblockComponent(): void {
    this.appService.unblockComponent(this.componentName);
  }
}
