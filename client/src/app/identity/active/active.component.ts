import { AfterViewInit, Component } from '@angular/core';
import { ActiveAccount } from '../../shared/models/activeaccount';
import { ActivatedRoute, RouterLinkActive, Router } from '@angular/router';
import { IdentityService } from '../identity.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-active',
  templateUrl: './active.component.html',
  styleUrl: './active.component.scss'
})
export class ActiveComponent implements AfterViewInit {
  constructor(private _route: ActivatedRoute, private _service:IdentityService, private _toastr:ToastrService,private router:Router) { }
  ngAfterViewInit(): void {
    this._route.queryParams.subscribe(params => {
      this.activeparam.email = params['email'];
      this.activeparam.token = params['code'];
    });
    this._service.active(this.activeparam).subscribe({
      next: (response) => {
        console.log(response);
        this._toastr.success('Account activated successfully', 'Success');
        this.router.navigateByUrl('/Account/login');
      },
      error: (error: any) => {
        console.log(error);
        
      }
    });
  }
  activeparam = new ActiveAccount();

}
