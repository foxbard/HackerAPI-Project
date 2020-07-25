import { IItem } from '../interfaces/IItem';

import { HttpClient, HttpHandler } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController} from '@angular/common/http/testing';
import { ErrorService } from './error.service';
import { HackerApiService } from './hackerapi.service';
import { TestBed, async, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';



describe('HackerApiService', () => {

  let hackerApiService: HackerApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
      ],
      declarations: [
      ],
      providers: [
        HackerApiService,
      ]
    });
  });

  beforeEach(() => {

    hackerApiService = TestBed.inject(HackerApiService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('HackerAPIService should be defined', () => {
    expect(hackerApiService).toBeDefined();
  });


  it('should fetch newest stories as an Observable',
    async(inject([HttpTestingController, HackerApiService],
      (httpClient: HttpTestingController, hackerService: HackerApiService) => {
        hackerService.getNewestStoriesItems().subscribe((stories: any) => {

          expect(stories.length).toBeGreaterThan(1);
        });
    })));

  it('should fetch top stories as an Observable',
  async(inject([HttpTestingController, HackerApiService],
    (httpClient: HttpTestingController, hackerService: HackerApiService) => {
      hackerService.getTopStoriesItems().subscribe((stories: any) => {

        expect(stories.length).toBeGreaterThan(1);
      });
  })));

  it('should fetch best stories as an Observable',
    async(inject([HttpTestingController, HackerApiService],
      (httpClient: HttpTestingController, hackerService: HackerApiService) => {
        hackerService.getBestStoriesItems().subscribe((stories: any) => {

          expect(stories.length).toBeGreaterThan(1);
        });
    })));

  it('should fetch story as an Observable',
  async(inject([HttpTestingController, HackerApiService],
    (httpClient: HttpTestingController, hackerService: HackerApiService) => {
      hackerService.getStoryByItemId(23941710).subscribe((story: IItem) => {

        expectAsync(story.title).toBeResolved();
      });

  })));

  });

