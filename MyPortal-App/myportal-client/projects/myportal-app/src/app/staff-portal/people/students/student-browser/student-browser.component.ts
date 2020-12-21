import { PortalViewComponent } from './../../../../shared/portal-view/portal-view.component';
import { Component, OnInit, Renderer2, ElementRef, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-student-browser',
  templateUrl: './student-browser.component.html',
  styleUrls: ['./student-browser.component.css']
})
export class StudentBrowserComponent extends PortalViewComponent implements OnInit, OnDestroy {

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef) {
    super(renderer, hostElement);
   }

  ngOnInit(): void {

  }
}
