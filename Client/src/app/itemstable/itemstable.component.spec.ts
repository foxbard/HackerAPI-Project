import { HttpClient, HttpHandler } from '@angular/common/http';
import { ErrorService } from './../../services/error.service';
import { HackerApiService } from './../../services/hackerapi.service';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';

import { ItemstableComponent } from './itemstable.component';

describe('ItemstableComponent', () => {
  let component: ItemstableComponent;
  let fixture: ComponentFixture<ItemstableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemstableComponent ],
      imports: [
        NoopAnimationsModule,
        MatPaginatorModule,
        MatSortModule,
        MatTableModule,
      ],
      providers: [
        HackerApiService,
        ErrorService,
        HttpClient,
        HttpHandler,
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemstableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should compile', () => {
    expect(component).toBeTruthy();
  });
});
