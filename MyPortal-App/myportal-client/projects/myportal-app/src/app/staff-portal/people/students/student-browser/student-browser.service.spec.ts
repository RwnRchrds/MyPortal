import { TestBed } from '@angular/core/testing';

import { StudentBrowserService } from './student-browser.service';

describe('StudentBrowserService', () => {
  let service: StudentBrowserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentBrowserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
