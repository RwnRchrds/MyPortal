/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { AlertService } from './alert.service';

describe('Service: Alert', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AlertService]
    });
  });

  it('should ...', inject([AlertService], (service: AlertService) => {
    expect(service).toBeTruthy();
  }));
});
