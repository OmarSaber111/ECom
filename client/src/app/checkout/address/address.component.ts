import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss']
})
export class AddressComponent implements OnInit {
  constructor(private _service: CheckoutService){}
  ngOnInit(): void {
    this._service.getaddress().subscribe({
      next: (response:any) => {
        this.address.patchValue(response);
        console.log('response',response);
      },
      error: (error) => {
        console.log(error);
      }
    });  
  }
  canEdit = false;
  @Input() address!: FormGroup;
  updateAddress() {
    if (this.address.valid) {
      this._service.updateAddress(this.address.value).subscribe({
        next: (response) => {
          console.log(response);
        },
        error: (error) => {
          console.log(error);
        }
      });
    } 
  }

}
