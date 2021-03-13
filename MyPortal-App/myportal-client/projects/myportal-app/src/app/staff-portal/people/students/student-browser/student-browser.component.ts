import { PortalViewDirective } from '../../../../_directives/portal-view/portal-view.directive';
import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';

@Component({
  selector: 'app-student-browser',
  templateUrl: './student-browser.component.html',
  styleUrls: ['./student-browser.component.css']
})
export class StudentBrowserComponent extends PortalViewDirective implements OnInit {

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef) {
    super(renderer, hostElement);
   }

  ngOnInit(): void {
  }
}
