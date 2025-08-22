import { Component, EventEmitter, input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css',
})
export class PaginationComponent {
  pageNumber = input(0);
  totalPages = input(0);
  totalCount = input(0);
  hasPreviousPage = input(false);
  hasNextPage = input(false);

  @Output() pageChange = new EventEmitter<number>();

  goToFirst() {
    if (this.pageNumber() > 1) {
      this.pageChange.emit(1);
    }
  }

  goToPrevious() {
    if (this.pageNumber() > 1) {
      this.pageChange.emit(this.pageNumber() - 1);
    }
  }

  goToNext() {
    if (this.pageNumber() < this.totalPages()) {
      this.pageChange.emit(this.pageNumber() + 1);
    }
  }

  goToLast() {
    if (this.pageNumber() < this.totalPages()) {
      this.pageChange.emit(this.totalPages());
    }
  }
}
