import { TestBed } from '@angular/core/testing';

import { UserViewService } from './user-view.service';

describe('UserViewService', () => {
  let service: UserViewService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserViewService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
