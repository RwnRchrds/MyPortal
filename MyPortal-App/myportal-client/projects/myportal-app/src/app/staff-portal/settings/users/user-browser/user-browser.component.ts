import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {PortalViewDirective} from '../../../../_directives/portal-view/portal-view.directive';

@Component({
  selector: 'app-user-browser',
  templateUrl: './user-browser.component.html',
  styleUrls: ['./user-browser.component.css']
})
export class UserBrowserComponent extends PortalViewDirective implements OnInit {

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef) {
    super(renderer, hostElement);
  }

  ngOnInit(): void {
  }

}
