import { TestBed } from '@angular/core/testing';

import { StudentViewService } from './student-view.service';

describe('StudentViewService', () => {
  let service: StudentViewService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentViewService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
