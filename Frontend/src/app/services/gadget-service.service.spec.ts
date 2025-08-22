import { TestBed } from '@angular/core/testing';

import { GadgetServiceService } from './gadget-service.service';

describe('GadgetServiceService', () => {
  let service: GadgetServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GadgetServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
