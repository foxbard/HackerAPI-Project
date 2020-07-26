import { Observable } from 'rxjs';
import { HackerApiService } from './../../services/hackerapi.service';

import { IItem } from './../../interfaces/IItem';
import { IUser } from './../../interfaces/IUser';

import { Component, OnInit, ViewChild, Input, AfterViewInit } from '@angular/core';


import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { ItemstableDataSource} from './itemstable-datasource';


@Component({
  selector: 'app-itemstable',
  templateUrl: './itemstable.component.html',
  styleUrls: ['./itemstable.component.sass']
})
export class ItemstableComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<IItem>;
  @Input() dataSource: ItemstableDataSource;
  @Input() data: IItem[];

  @Input() spinnerVisibiltyState: string;
  @Input() tableVisibilityState: string;


  public loading: boolean;
  public loaded: boolean;


  public selectedItem: any;
  public selectedUser: IUser;
  public topLevelUserId: string;
  public Users: IUser[] = [];

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['id', 'by', 'type', 'title', 'link'];

  constructor(private hackerApiService?: HackerApiService){
    this.loading = false;
    this.loaded = false;
    this.tableVisibilityState = 'hidden';
    this.spinnerVisibiltyState = 'visible';
  }

  ngOnInit(): void {
    this.callNewestStoriesApi();
  }

  ngAfterViewInit(): void {
    //
  }

  /**
   * @description calls newest stories from API, is used for intial table population on init.
   */
  callNewestStoriesApi(): void{

      this.hackerApiService.getCurrentApiData().subscribe(data => {
        console.log('APi DATA: ', data);
        if (data !== undefined){
          try{
            this.data = data;
            console.log('Data from Api Cache For Table: ', this.data);
            this.data.filter((item) =>  item !== null);
            this.dataSource = new ItemstableDataSource(this.data);
            this.dataSource.sort = this.sort;
            this.dataSource.paginator = this.paginator;
            if (this.table !== undefined){
              this.table.dataSource = this.dataSource;
            }
          }
          catch (err){
              console.error('JSON Parsing failed', err);
          }
        }
      });
  }


  /**
   * @description Handles on click event for user name;
   */
  clickedUser(event): void{
    const user = event.target.innerText;
    this.hackerApiService.getUserById(user).subscribe(res => {
      this.selectedUser = res;
      this.hackerApiService.setUserForCard(res);
      this.hackerApiService.setBuffer(false);
    });
  }

  /**
   * @description sets filtered Data for story title filtering
   * @param event {Event}
   */
  showFilteredData(event): void{
    console.log('Event Emitted: ', event);
    this.hackerApiService.getFilterdData().subscribe( res => this.data = res);
  }
}
