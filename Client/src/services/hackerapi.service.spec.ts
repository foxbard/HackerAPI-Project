import { TestBed } from '@angular/core/testing';

import { HackerApiService } from './hackerapi.service';
import { HttpClient, HttpHandler } from '@angular/common/http';

describe('HackerapiService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
      ],
      declarations: [
      ],
      providers: [
        HackerApiService,
        HttpClient,
        HttpHandler,
      ]
    });
  });
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HackerApiService = TestBed.inject(HackerApiService);
    expect(service).toBeTruthy();
  });
});
