import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginationComponent } from './component/pagination/pagination.component';


@NgModule({
  declarations: [
    PaginationComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot()
  ],
  exports: [
    PaginationModule,PaginationComponent
  ]
})
export class SharedModule { }
