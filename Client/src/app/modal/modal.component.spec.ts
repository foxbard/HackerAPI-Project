import { MatMenu, MatMenuModule } from '@angular/material/menu';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { ErrorService } from './../../services/error.service';
import { HackerApiService } from './../../services/hackerapi.service';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalComponent } from './modal.component';

describe('ModalComponent', () => {
  let component: ModalComponent;
  let fixture: ComponentFixture<ModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        ModalComponent,
      ],
      imports: [
      // BrowserModule,
      // AppRoutingModule,
      // HttpClientModule,
      // BrowserAnimationsModule,
      // ReactiveFormsModule,
      // FormsModule,
      // MatSliderModule,
      // LayoutModule,
      // MatToolbarModule,
      // MatButtonModule,
      // MatSidenavModule,
      // MatIconModule,
      // MatListModule,
      // MatTableModule,
      // MatPaginatorModule,
      // MatSortModule,
      // MatCardModule,
      // MatProgressSpinnerModule,
      // MatAutocompleteModule,
      // MatFormFieldModule,
      // MatInputModule,
      MatMenuModule,
      // MatProgressBarModule,
      ],
      providers: [
        HackerApiService,
        ErrorService,
        HttpClient,
        HttpHandler
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
