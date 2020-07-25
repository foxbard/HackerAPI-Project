import { MatMenuModule } from '@angular/material/menu';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { HackerApiService } from './../../services/hackerapi.service';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FilterTitlesComponent } from './filter-titles.component';

describe('FilterTitlesComponent', () => {
  let component: FilterTitlesComponent;
  let fixture: ComponentFixture<FilterTitlesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        FilterTitlesComponent,

      ],
      imports: [
        MatAutocompleteModule,
        MatMenuModule,
      ],
      providers: [
        HackerApiService,
        HttpClient,
        HttpHandler
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FilterTitlesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
