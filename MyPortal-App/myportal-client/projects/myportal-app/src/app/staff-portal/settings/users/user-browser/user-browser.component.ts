import {Component, ElementRef, OnDestroy, OnInit, Renderer2} from '@angular/core';
import {PortalViewComponent} from '../../../../shared/portal-view/portal-view.component';

@Component({
  selector: 'app-user-browser',
  templateUrl: './user-browser.component.html',
  styleUrls: ['./user-browser.component.css']
})
export class UserBrowserComponent extends PortalViewComponent implements OnInit, OnDestroy {

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef) {
    super(renderer, hostElement);
  }

  ngOnInit(): void {

  }
}
