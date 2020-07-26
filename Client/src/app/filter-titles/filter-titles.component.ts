import { HackerApiService } from '../../services/hackerapi.service';
import { IItem } from '../../interfaces/IItem';
import { ItemstableDataSource } from '../itemstable/itemstable-datasource';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import {FormControl} from '@angular/forms';

import { Observable } from 'rxjs';
import { startWith, map, tap, filter } from 'rxjs/operators';

@Component({
  selector: 'app-filter-titles',
  templateUrl: './filter-titles.component.html',
  styleUrls: ['./filter-titles.component.sass']
})
export class FilterTitlesComponent implements OnInit {

  filterControl = new FormControl();
  filteredOptions: Observable<any>;


  @Input() userData: any[] = [];
  @Output() updateFilteredData: IItem[];
  @Output() updateFiltered = new EventEmitter();


  constructor(private hackerApiService: HackerApiService) { }

  ngOnInit(): void {
    this.filteredOptions = this.filterControl.valueChanges
      .pipe(
        // tap((v) => {console.log('Value: ', v); }),
        startWith(''),
        map(value => this._filter(value))
      );
  }

  /**
   * @description filters titles from IItem[] to display in filter dropdown
   * @param value {string} string value to filter
   */
  private _filter(value: string): any{
    let filterValue = '';
    this.hackerApiService.getCurrentApiData().subscribe((res: any) => this.userData = res);
    if (value.toLowerCase !== undefined){
        filterValue = value.toLowerCase();
        if (this.userData !== undefined){
          this.updateFilteredData = this.userData.filter(story => {
            if (story !== null){
              if (story.title.toLowerCase().includes(filterValue)){
                return true;
              }
            }
          });
          this.hackerApiService.setFilteredData(this.updateFilteredData);
          this.updateFiltered.emit(this.updateFilteredData);
          return this.updateFilteredData;
        }
    }
    if (filterValue === ''){
      return [''];
    }
  }


}
