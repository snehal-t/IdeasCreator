import { TestBed } from '@angular/core/testing';

import { IdeasapiService } from './ideasapi.service';

describe('IdeasapiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: IdeasapiService = TestBed.get(IdeasapiService);
    expect(service).toBeTruthy();
  });
});
