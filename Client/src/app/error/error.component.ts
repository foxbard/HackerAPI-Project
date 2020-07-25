import { ErrorService } from './../../services/error.service';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-error',
  templateUrl: './error.component.html',
  styleUrls: ['./error.component.sass']
})
export class ErrorComponent implements OnInit {

  @Input() showError: boolean;
  @Input() errorMessage: string;

  constructor(private errorService: ErrorService) {
    this.showError = false;
    this.errorMessage = '';
  }

  ngOnInit(): void {
    this.errorService.setErrorMsg(this.errorMessage);
    this.errorService.setShowErrorMsg(this.showError);
    this.setErrorMessageValues();
  }

  /**
   * @description close out error message and clear message string buffer;
   */
  close(): void{
    this.showError = false;
    this.errorMessage = '';
  }

  /**
   * @description sets error message values.
   */
  setErrorMessageValues(): void{
    this.errorService.errorMsgValue.subscribe(msg => this.errorMessage = msg);
    this.errorService.showErrorMsg.subscribe(value => this.showError = value);
  }

}
