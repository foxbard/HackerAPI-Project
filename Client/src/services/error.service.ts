import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';




@Injectable()
export class ErrorService{

  private errorMsg = new BehaviorSubject('');
  private errorMsgState = new BehaviorSubject(false);

  errorMsgValue = this.errorMsg.asObservable();
  showErrorMsg = this.errorMsgState.asObservable();

  /**
   * @description sets error message on Observable
   * @param message [string]
   */
  setErrorMsg(message: string): void{
    this.errorMsg.next(message);
  }

  /**
   * @description sets error if error message is show or hidden on Observable
   * @param message [boolean]
   */
  setShowErrorMsg(value: boolean): void{
    this.errorMsgState.next(value);
  }

}
