import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-success',
  standalone: false,
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.scss']
})
export class Success implements OnInit {
 orderId:number = 0;
 constructor( private route:ActivatedRoute) {}
  ngOnInit(): void {
    debugger;
  this.route.queryParams.subscribe(param => {
    this.orderId = param['orderId'];
  }); 
  }

}
