import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RoleBrowserService {

  showSearchComponent = true;
  showCreateComponent = false;

  showCreate(): void {
    this.showSearchComponent = false;
    this.showCreateComponent = true;
  }

  showSearch(): void {
    this.showCreateComponent = false;
    this.showSearchComponent = true;
  }

  reset(): void {
    this.showSearch();
  }

  constructor() { }
}
