import {Directive, ElementRef, OnDestroy, Renderer2} from '@angular/core';
import {InjectorService} from '../../../_services/injector.service';

@Directive({
  selector: '[appPortalView]'
})
export abstract class PortalViewDirective implements OnDestroy {

  protected constructor(protected renderer: Renderer2, protected hostElement: ElementRef) {
    const injector = InjectorService.getInjector();
    console.log('Injector here:');
    console.log(injector);
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
