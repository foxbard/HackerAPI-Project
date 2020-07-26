import { HttpClient, HttpHandler } from '@angular/common/http';
import { ErrorService } from './../../services/error.service';
import { HackerApiService } from './../../services/hackerapi.service';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';

import { ItemstableComponent } from './itemstable.component';
import { DebugElement } from '@angular/core';
import { By } from '@angular/platform-browser';

describe('ItemstableComponent', () => {
  let component: ItemstableComponent;
  let fixture: ComponentFixture<ItemstableComponent>;
  let debugElement: DebugElement;
  let serviceStubGetStoriesItems: any;

  beforeEach(async(() => {

    serviceStubGetStoriesItems = {
      getNewestStoriesItems: () => [
        {
          id: 0,
          title: 'test',
        }
      ]
    }

    TestBed.configureTestingModule({
      declarations: [ ItemstableComponent ],
      imports: [
        NoopAnimationsModule,
        MatPaginatorModule,
        MatSortModule,
        MatTableModule,
      ],
      providers: [
        { provide: HackerApiService, useValue: serviceStubGetStoriesItems},
        ErrorService,
        HttpClient,
        HttpHandler,
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemstableComponent);
    component = fixture.componentInstance;
    debugElement = fixture.debugElement;
    fixture.detectChanges();
  });

  it('should compile', () => {
    expect(component).toBeTruthy();
  });

  it('should be loading', () => {
    expect(component.loading).toBeFalsy();
  });

  it('should show spinner and hide table when loading data', () => {
    expect(component.spinnerVisibiltyState).toEqual('visible');
    expect(component.tableVisibilityState).toEqual('hidden');
  });

  it('should be loaded', () => {
    expect(component.loading).toBeFalsy();
  });

  it('should call callNewestStoriesApi()', () => {
    expect(component.callNewestStoriesApi).toBeDefined();
    expect(component.callNewestStoriesApi).toBeTruthy();
  });


});
