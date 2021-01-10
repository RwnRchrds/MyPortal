import { TestBed } from '@angular/core/testing';

import { UserBrowserService } from './user-browser.service';

describe('UserBrowserService', () => {
  let service: UserBrowserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserBrowserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
