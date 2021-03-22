import {Directive, Input, OnInit} from '@angular/core';

@Directive({
  selector: '[appNgInit]'
})
export class NgInitDirective implements OnInit {

  @Input() values: any = {};

  @Input() ngInit;

  ngOnInit(): void {
    if (this.ngInit) {
      this.ngInit();
    }
  }

  constructor() { }

}
