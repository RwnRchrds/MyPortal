import { Directive } from '@angular/core';
import {AuthService} from '../../_services/auth.service';
import {Reset} from '../../_models/reset';

@Directive({
  selector: '[appPortalViewService]'
})
export abstract class PortalViewServiceDirective implements Reset {

  abstract reset(): void;

  constructor(private authService: AuthService) { }
}
