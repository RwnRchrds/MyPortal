import { PortalViewDirective } from '../../../../shared/portal-view/portal-view.directive';
import { Component, OnInit, Renderer2, ElementRef } from '@angular/core';
import { StudentBrowserService } from './student-browser.service';

@Component({
  selector: 'app-student-browser',
  templateUrl: './student-browser.component.html',
  styleUrls: ['./student-browser.component.css']
})
export class StudentBrowserComponent extends PortalViewDirective implements OnInit {

  viewService: StudentBrowserService;

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef, studentBrowserService: StudentBrowserService) {
    super(renderer, hostElement, studentBrowserService);
   }

  ngOnInit(): void {
  }
}
