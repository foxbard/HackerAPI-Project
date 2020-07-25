import { ErrorService } from './../../services/error.service';
import { HackerApiService } from './../../services/hackerapi.service';
import { IUser } from './../../interfaces/IUser';
import { Component, OnInit, Input, AfterViewInit, ViewChild } from '@angular/core';
import { IItem } from 'src/interfaces/IItem';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.sass']
})
export class ModalComponent implements OnInit, AfterViewInit {

  @Input() itemValue: IItem;
  public user: IUser;

  public buffering = false;
  public  showError = false;
  public  errorMessage = 'This is a test message';

  constructor(private hackerApiService: HackerApiService, private errorService: ErrorService) {
  }

  ngOnInit(): void {
    this.hackerApiService.getUserForCard().subscribe(user =>  this.user = user);
  }

  ngAfterViewInit(): void {
    this.hackerApiService.getUserForCard().subscribe(
      res => {
        this.hackerApiService.bufferState.subscribe(buffer => this.buffering = buffer);
        console.log('User updated in Modal Data: ', res);
        this.user = res;
      }
    );
  }

  selectStoryEvent(event): void{
    console.log(event);
    console.log('ItemValue: ', this.itemValue);
    this.hackerApiService.getStoryByItemId(event.target.innerText)
      .subscribe((item: IItem) => {
        console.log('URL ====>  ', item.url);
        if (item.url !== undefined && item.url !== null){
          window.open(item.url.toString());
        }else{
          console.error('No Url Defined: ', item);
          this.errorMessage = 'No URL defined id: ' + item.id;
          this.showError = true;
          this.errorService.setShowErrorMsg(this.showError);
          this.errorService.setErrorMsg(this.errorMessage);

        }
      });
  }

}
