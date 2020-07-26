import { DataSource } from '@angular/cdk/collections';
import { IUser } from './../interfaces/IUser';

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

// RXJS
import { Observable, throwError, Subject, AsyncSubject, BehaviorSubject, of } from 'rxjs';
import { catchError, retry, take, filter, switchMap, tap, first, map } from 'rxjs/operators';


// Interfaces
import { IItem } from '../interfaces/IItem';

// global reference for domain
const apiBaseURL = 'https://localhost:5001/api';
// const apiHackerBase = 'https://hacker-news.firebaseio.com/v0';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};




@Injectable()
export class HackerApiService {

  public exposedBaseURL = apiBaseURL;

  private selectedUser = new BehaviorSubject({
    id: 'Select a author from the "By" Column to see more info about that user'
    , about: ''
    , submitted: ['']
    , delay: 0
    , created: 0
    , karma: 0
  });

  private isBuffering = new BehaviorSubject(false);
  private filteredData = new BehaviorSubject([]);
  private apiData = new BehaviorSubject<IItem[]>([]);

  currentSelectedUser = this.selectedUser.asObservable();
  bufferState = this.isBuffering.asObservable();
  filteredDataState = this.filteredData.asObservable();
  currentApiData = this.apiData.asObservable();

  constructor(private http: HttpClient){}

  /**
   * @description basic error handling
   * @param operation operation data
   * @param result result of Type<T>
   */
  private errorHandler<T>(operation = 'operation', result?: T){
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }

  /**
   * @description gets current data for Items in Observable
   */
  getCurrentApiData(): Observable<IItem[]>{
    console.log('Current Api Data Cache: ', this.currentApiData);
    return this.currentApiData;
  }

  /**
   * @description Stores current data in Observable
   */
  setCurrentApiData(itemsArray: IItem[]): void{
    console.log('Setting Current Items Data: ', itemsArray);
    this.apiData.next(itemsArray);
  }




  /**
   * @description Returns latest stories with no caching.
   */
  getNewestStoriesItemsNoCache(): Observable<any>{
    return this.http.get<number[]>(`${apiBaseURL}/items/getNewStories/nocache`, {headers: httpOptions.headers})
      .pipe(
        tap((res) => console.log('Results:', res)),
        catchError(this.errorHandler('getNewestStoriesItems', []))
      );
  }

  /**
   * @description Returns latest stories.
   */
  getNewestStoriesItems(): Observable<any>{
    return this.http.get<number[]>(`${apiBaseURL}/items/getNewStories/`, {headers: httpOptions.headers})
      .pipe(
        catchError(this.errorHandler('getNewestStoriesItems', []))
      );
  }


  /**
   * @description Returns top stories.
   */
  getTopStoriesItems(): Observable<any>{
    return this.http.get<IItem>(`${apiBaseURL}/items/getTopStories`)
      .pipe(
        catchError(this.errorHandler('getTopStoriesItems', []))
      );
  }

  /**
   * @description Returns best stories.
   */
  getBestStoriesItems(): Observable<any>{
    return this.http.get<IItem>(`${apiBaseURL}/items/getBestStories`)
    .pipe(
      catchError(this.errorHandler('getBestStoriesItems', []))
    );
  }

  /**
   * @description returns singular story item from API Endpoint
   * @param id {number} id of item to retrieve from hackerAPI
   */
  getStoryByItemId(id: number): Observable<any>{
    return this.http.get<IItem>(`${apiBaseURL}/items/getStoryById/${id}`)
    .pipe(
      catchError(this.errorHandler('getStoryByItemId', []))
    );
  }

  /**
   * @description returns a user from endpoint
   * @param name {string} the user's unique username. case sensitive.
   */
  getUserById(name: string): Observable<any>{
    this.isBuffering.next(true);
    return this.http.get<IUser>(`${apiBaseURL}/users/getUserByName/${name}`)
    .pipe(
      tap((user) => console.log('Getting User: ', user)),
      catchError(this.errorHandler(`getUserById id=${name}`, []))
    );
  }

  /**
   * @description updates user card info
   */
  setUserForCard(user: IUser): void{
    // Update user info displayed in card
    console.log('Current User: ', user);
    this.selectedUser.next(user);
  }

  /**
   * @description gets user card info
   */
  getUserForCard(): Observable<IUser>{
    return this.currentSelectedUser;
  }

  /**
   * @description set display of progress buffer true or false;
   */
  setBuffer(value: boolean): void{
    this.isBuffering.next(value);
  }

  /**
   * @description sets filterd data for dropdown when filtering for story titles
   */
  setFilteredData(data: IItem[]): void{
    this.filteredData.next(data);
  }

  /**
   * @description gets filterd data for dropdown when filtering for story titles
   */
  getFilterdData(): Observable<IItem[]>{
    return this.filteredData;
  }


}
