import { TestBed } from '@angular/core/testing';

import { RoleBrowserService } from './role-browser.service';

describe('RoleBrowserService', () => {
  let service: RoleBrowserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RoleBrowserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
