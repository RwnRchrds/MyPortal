import { Component, OnInit, Renderer2, ElementRef, OnDestroy } from '@angular/core';
import {StaffHomepageService} from './staff-homepage.service';
import {PortalViewDirective} from '../../_directives/portal-view/portal-view.directive';

@Component({
  selector: 'app-staff-homepage',
  templateUrl: './staff-homepage.component.html',
  styleUrls: ['./staff-homepage.component.css']
})
export class StaffHomepageComponent extends PortalViewDirective implements OnInit, OnDestroy {

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef, private viewService: StaffHomepageService) {
    super(renderer, hostElement);
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    super.removeStyles();
  }

}
