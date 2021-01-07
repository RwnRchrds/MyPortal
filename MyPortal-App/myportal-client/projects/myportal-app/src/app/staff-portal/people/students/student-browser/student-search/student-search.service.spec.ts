import { TestBed } from '@angular/core/testing';

import { StudentSearchService } from './student-search.service';

describe('StudentSearchService', () => {
  let service: StudentSearchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentSearchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
