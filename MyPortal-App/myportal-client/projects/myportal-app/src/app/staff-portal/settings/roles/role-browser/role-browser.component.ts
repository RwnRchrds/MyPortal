import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {PortalViewDirective} from '../../../../shared/portal-view/portal-view.component';

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
