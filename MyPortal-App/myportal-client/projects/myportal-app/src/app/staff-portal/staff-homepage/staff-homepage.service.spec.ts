import { TestBed } from '@angular/core/testing';

import { StaffHomepageService } from './staff-homepage.service';

describe('StaffHomepageService', () => {
  let service: StaffHomepageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StaffHomepageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
