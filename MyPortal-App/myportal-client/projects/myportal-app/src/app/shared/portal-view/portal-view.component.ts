import {Directive, ElementRef, OnDestroy, Renderer2} from '@angular/core';

@Directive({

})
export abstract class PortalViewDirective implements OnDestroy {

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef) {
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
    this.removeStyles();
  }
}
