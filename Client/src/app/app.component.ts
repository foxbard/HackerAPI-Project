import { HackerApiService } from './../services/hackerapi.service';
import { Component, OnInit, AfterViewInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  title = 'hackerAPIViewer';

  constructor(private hackerApiService: HackerApiService){

  }
}
