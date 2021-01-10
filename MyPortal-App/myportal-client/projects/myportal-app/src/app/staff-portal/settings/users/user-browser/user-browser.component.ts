import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {UserBrowserService} from './user-browser.service';
import {PortalViewDirective} from '../../../../shared/portal-view/portal-view.directive';

@Component({
  selector: 'app-user-browser',
  templateUrl: './user-browser.component.html',
  styleUrls: ['./user-browser.component.css']
})
export class UserBrowserComponent extends PortalViewDirective implements OnInit {

  viewService: UserBrowserService;

  constructor(renderer: Renderer2, hostElement: ElementRef, userBrowserService: UserBrowserService) {
    super(renderer, hostElement, userBrowserService);
    this.viewService = userBrowserService;
  }

  ngOnInit(): void {
  }

}
