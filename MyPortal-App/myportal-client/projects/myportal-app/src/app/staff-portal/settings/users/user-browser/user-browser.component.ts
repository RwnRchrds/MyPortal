import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {PortalViewDirective} from '../../../../shared/portal-view/portal-view.directive';

@Component({
  selector: 'app-user-browser',
  templateUrl: './user-browser.component.html',
  styleUrls: ['./user-browser.component.css']
})
export class UserBrowserComponent extends PortalViewDirective implements OnInit {

  constructor(renderer: Renderer2, hostElement: ElementRef) {
    super(renderer, hostElement);
  }

  ngOnInit(): void {
  }

}
