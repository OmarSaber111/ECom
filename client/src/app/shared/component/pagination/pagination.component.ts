import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss'
})
export class PaginationComponent {
@Input() pageSize!: number;
@Input() totalCount!: number;
@Output() pageChanged = new EventEmitter<any>();
onPageChanged(event: any) {
    this.pageChanged.emit(event);
  }
}
