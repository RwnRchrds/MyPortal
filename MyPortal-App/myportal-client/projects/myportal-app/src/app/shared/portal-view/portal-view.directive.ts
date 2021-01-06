import {Directive, ElementRef, OnDestroy, Renderer2} from '@angular/core';
import {PortalViewServiceDirective} from './portal-view-service.directive';

@Directive({
  selector: '[appPortalView]'
})
export abstract class PortalViewDirective implements OnDestroy {

  protected constructor(protected renderer: Renderer2, protected hostElement: ElementRef,
                        protected viewService: PortalViewServiceDirective) {
    this.addStyles();
  }

  addStyles(): void
  {
    this.renderer.addClass(this.hostElement.nativeElement, 'h-100');
  }

  removeStyles(): void
  {
    this.renderer.removeClass(this.hostElement.nativeElement, 'h-100');
  }

  ngOnDestroy(): void {
    this.viewService.reset();
    this.removeStyles();
  }
}
