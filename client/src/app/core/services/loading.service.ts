import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
 requestsCount = 0;
  constructor(private _service:NgxSpinnerService) { }
 loading() {
  this._service.show(undefined, {
    bdColor: 'rgba(0, 0, 0, 0.8)',
    size: 'large',
    color: '#fff',
    type: 'square-jelly-box',
    fullScreen: true,
    template: 'Loading, please wait...'
  });

  this.requestsCount++;
}
  hideloader() {
      this.requestsCount--;
      if (this.requestsCount <= 0) {
          this._service.hide();
          this.requestsCount = 0;
      }
  }
}
