import { PortalViewDirective } from '../../shared/portal-view/portal-view.directive';
import { Component, OnInit, Renderer2, ElementRef, OnDestroy } from '@angular/core';
import {StaffHomepageService} from './staff-homepage.service';

@Component({
  selector: 'app-staff-homepage',
  templateUrl: './staff-homepage.component.html',
  styleUrls: ['./staff-homepage.component.css']
})
export class StaffHomepageComponent extends PortalViewDirective implements OnInit, OnDestroy {

  constructor(renderer: Renderer2, hostElement: ElementRef, private viewService: StaffHomepageService) {
    super(renderer, hostElement);
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    super.removeStyles();
  }

}
