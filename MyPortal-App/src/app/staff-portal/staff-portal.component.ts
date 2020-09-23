import {Component, OnDestroy, OnInit, Renderer2} from '@angular/core';
import {PortalRootDirective} from '../shared/portal-root.directive';

@Component({
  selector: 'app-staff-portal',
  templateUrl: './staff-portal.component.html',
  styleUrls: ['./staff-portal.component.css']
})
export class StaffPortalComponent extends PortalRootDirective implements OnInit, OnDestroy {

  displayName: string = 'Mrs L Sprague';

  constructor(renderer: Renderer2) {
    super(renderer);
  }

  ngOnInit(): void {
    super.addBodyClasses();
  }

  ngOnDestroy(): void {
    super.removeBodyClasses();
  }

}
