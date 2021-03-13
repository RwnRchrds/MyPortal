import {Component, ElementRef, OnDestroy, OnInit, Renderer2} from '@angular/core';
import {PortalViewDirective} from '../../../../_directives/portal-view/portal-view.directive';

@Component({
  selector: 'app-role-browser',
  templateUrl: './role-browser.component.html',
  styleUrls: ['./role-browser.component.css']
})
export class RoleBrowserComponent extends PortalViewDirective implements OnInit {

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef) {
    super(renderer, hostElement);
  }

  ngOnInit(): void {
  }
}
