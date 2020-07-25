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
  // public sort: any;
  // public paginator: any;
  // public table: any;



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
    this.callNewestStoriesApi();
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
   * @description gets new stories
   */
  callNewestStoriesApi(): void{

    this.hackerApiService.getNewestStoriesItems().forEach( ids => {
      this.callProcessStories(ids);
    });
  }

  /**
   * @description gets top stories
   */
  callTopStoriesApi(): void{
    this.hackerApiService.getTopStoriesItems().forEach( ids => {
      this.callProcessStories(ids);
    });
  }

  /**
   * @description gets best stories
   */
  callBestStoriesApi(): void{
    this.hackerApiService.getBestStoriesItems().forEach( ids => {
      this.callProcessStories(ids);
    });
  }

  /**
   * @description processes story item ids and sets table/spinner visibility status.
   * @param ids number[] value to process
   */
  callProcessStories(ids: number[]): void{
    ids.forEach(id => {
      if (id !== null){
        this.hackerApiService.getStoryByItemId(id)
        .toPromise()
        .then( res => {
          this.data.push(res); })
        .then(() => {
          this.tableVisibilityState = 'visible';
          this.spinnerVisibiltyState = 'hidden';
          this.hackerApiService.setCurrentApiData(this.data);
        })
        .catch( err => {
          console.error('Unable to retrieve item based on id of ', id, err);
        });
      }
    });
  }



  ngOnDestory(): void {
    this.mobileQuery.removeListener(this.mobileQueryListener);
  }

}
