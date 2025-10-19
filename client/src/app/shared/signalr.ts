import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class Signalr {
  private hubConnection!: signalR.HubConnection;

  constructor(private toastr: ToastrService) {}

  // Start connection
  public startConnection(): void {
    debugger;
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44375/ProductHub') // 👈 your backend hub URL
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connection Started'))
      .catch(err => console.log('Error while starting connection: ' + err));

    this.addProductListener();
  }

  // Listen for messages from the server
  private addProductListener(): void {
    this.hubConnection.on('NotifyProductUpdate', (message: string) => {
      console.log('Product update:', message);
      this.toastr.info(message, 'Product Notification'); // optional toastr
    });
  }
  
}
