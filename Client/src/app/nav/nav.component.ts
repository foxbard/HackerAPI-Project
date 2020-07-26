import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {MediaMatcher} from '@angular/cdk/layout';
import {ChangeDetectorRef,  OnDestroy} from '@angular/core';


// Services
import { HackerApiService } from './../../services/hackerapi.service';
import { Observable } from 'rxjs';
import { map, shareReplay, take } from 'rxjs/operators';

import { IItem } from 'src/interfaces/IItem';
import { ItemstableDataSource } from '../itemstable/itemstable-datasource';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.sass']
})
export class NavComponent implements OnInit{

  public mobileQuery: MediaQueryList;
  private mobileQueryListener : () => void;
  public pageTitle = 'Now Viewing Newest Stories';

  public typeStories = 'new';
  public loading: boolean;
  public loaded: boolean;
  public tableVisibilityState: string;
  public spinnerVisibiltyState: string;


  public Users: any;
  public data: IItem[] = [];
  public dataSource: ItemstableDataSource;
  



  constructor(
        private hackerApiService: HackerApiService,
        private changeDetectorRef: ChangeDetectorRef,
        private media: MediaMatcher
      ){
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this.mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this.mobileQueryListener);
  }

  ngOnInit(): void{
    this.callNewestStories();
  }

  /**
   * @description on clicks updates title and calls appropriate category for item data.
   */
  updateTitle(value: string): void{
    this.data = [];
    this.tableVisibilityState = 'hidden';
    this.spinnerVisibiltyState = 'visible';
    switch (value){
      case 'new': {
        this.pageTitle = `Now Viewing Newest Stories`;
        this.typeStories = 'new';
        this.callNewestStoriesApi();
        break;
      }
      case 'top':{
        this.pageTitle = `Now Viewing Top Stories`;
        this.typeStories = 'top';
        this.callTopStoriesApi();
        break;
      }
      case 'best':{
        this.pageTitle = `Now Viewing Best Stories`;
        this.typeStories = 'best';
        this.callBestStoriesApi();
        break;
      }
      default :{
        console.log('Unknown value for typeStories');
      }
    }
    // update data being sent
  }


  /**
   * @description loads intial stories and caches
   */
  callNewestStories(): void{

    this.hackerApiService.getNewestStoriesItems().subscribe((items) => {
      try{
        this.data = items;
        console.log('Data Retrieved: ', this.data, items);
        this.tableVisibilityState = 'visible';
        this.spinnerVisibiltyState = 'hidden';
        this.hackerApiService.setCurrentApiData(this.data);
      }catch (err){
        console.error('JSON parsing has failed!', err);
      }
    }).add(() => {
        // Load other items into backend cache;
        this.hackerApiService.getTopStoriesItems().subscribe();
        this.hackerApiService.getBestStoriesItems().subscribe();
    });
  }

  /**
   * @description gets new stories
   */
  callNewestStoriesApi(): void{
      this.hackerApiService.getNewestStoriesItems().subscribe(stories => {
        this.callProcessStories(stories);
      })
  }

  /**
   * @description gets top stories
   */
  callTopStoriesApi(): void{
    this.hackerApiService.getTopStoriesItems().subscribe(stories => {
      console.log('%c TOPSTORIES DATA: ======> ', ('color: green'), stories);
      this.callProcessStories(stories);
    });
  }

  /**
   * @description gets best stories
   */
  callBestStoriesApi(): void{
    this.hackerApiService.getBestStoriesItems().subscribe( stories => {
      this.callProcessStories(stories);
    });
  }

  /**
   * @description processes story item ids and sets table/spinner visibility status.
   * @param ids number[] value to process
   */
  callProcessStories(stories: IItem[]): void{
    
    this.tableVisibilityState = 'visible';
    this.spinnerVisibiltyState = 'hidden';
    this.hackerApiService.setCurrentApiData(stories);
  }



  ngOnDestory(): void {
    this.mobileQuery.removeListener(this.mobileQueryListener);
  }

}
