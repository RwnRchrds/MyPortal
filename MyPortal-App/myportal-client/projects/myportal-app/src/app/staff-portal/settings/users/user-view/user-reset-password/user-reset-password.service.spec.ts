import { TestBed } from '@angular/core/testing';

import { UserResetPasswordService } from './user-reset-password.service';

describe('UserResetPasswordService', () => {
  let service: UserResetPasswordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserResetPasswordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
