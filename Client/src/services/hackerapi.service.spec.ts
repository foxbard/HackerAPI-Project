import { IItem } from 'src/interfaces/IItem';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed, async } from '@angular/core/testing';

import { HackerApiService } from './hackerapi.service';
import { HttpClient, HttpHandler } from '@angular/common/http';


describe('HackerapiService', () => {

  let service: HackerApiService;
  let httpMock: HttpTestingController;

  beforeEach( async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      declarations: [
      ],
      providers: [
        HackerApiService,
        HttpClient,
        HttpHandler,
      ]
    });

    service = TestBed.inject(HackerApiService);
    httpMock = TestBed.inject(HttpTestingController);
  }));

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should define getNewestStoriesItems', () => {
    expect(service.getNewestStoriesItems).toBeDefined();
  });

  it('should define getTopStoriesItems', () => {
    expect(service.getTopStoriesItems).toBeDefined();
  });

  it('should define getNewestStoriesItems', () => {
    expect(service.getBestStoriesItems).toBeDefined();
  });
});
