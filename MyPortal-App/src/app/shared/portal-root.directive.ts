import {Directive, Renderer2} from '@angular/core';

@Directive({
  selector: '[appPortalRoot]'
})
export class PortalRootDirective {

  constructor(protected renderer: Renderer2) {

  }

  addBodyClasses(): void {
    this.renderer.addClass(document.body, 'header-fixed');
    this.renderer.addClass(document.body, 'header-mobile-fixed');
    this.renderer.addClass(document.body, 'subheader-enabled');
    this.renderer.addClass(document.body, 'subheader-fixed');
    this.renderer.addClass(document.body, 'aside-enabled');
    this.renderer.addClass(document.body, 'aside-fixed');
    this.renderer.addClass(document.body, 'aside-minimize-hoverable');
    this.renderer.addClass(document.body, 'aside-minimize');
  }

  removeBodyClasses(): void {
    this.renderer.removeClass(document.body, 'header-fixed');
    this.renderer.removeClass(document.body, 'header-mobile-fixed');
    this.renderer.removeClass(document.body, 'subheader-enabled');
    this.renderer.removeClass(document.body, 'subheader-fixed');
    this.renderer.removeClass(document.body, 'aside-enabled');
    this.renderer.removeClass(document.body, 'aside-fixed');
    this.renderer.removeClass(document.body, 'aside-minimize-hoverable');
  }

}
