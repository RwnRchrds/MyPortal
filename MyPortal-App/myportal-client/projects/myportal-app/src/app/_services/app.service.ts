import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor() { }

  blockComponent(uiComponent: string): void {
    // @ts-ignore
    KTApp.block(uiComponent);
  }

  unblockComponent(uiComponent: string): void {
    // @ts-ignore
    KTApp.unblock(uiComponent);
  }

  blockPage(): void {
    // @ts-ignore
    KTApp.blockPage();
  }

  unblockPage(): void {
    // @ts-ignore
    KTApp.unblockPage();
  }

  parseGuid(str: string): string {
    const parts = [];
    parts.push(str.slice(0, 8));
    parts.push(str.slice(8, 12));
    parts.push(str.slice(12, 16));
    parts.push(str.slice(16, 20));
    parts.push(str.slice(20, 32));
    return parts.join('-');
}
}
