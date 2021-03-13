import {Injectable, Injector} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class InjectorService {

  private static injector: Injector;

  constructor() { }

  static setInjector(injector: Injector): void {
    InjectorService.injector = injector;
  }

  static getInjector(): Injector {
    return InjectorService.injector;
  }
}
