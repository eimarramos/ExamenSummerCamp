import { Component, EventEmitter, input, Output } from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

export interface PaginationProps {
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css',
})
export class PaginationComponent {
  props = input<PaginationProps>();

  pageSizeOptions = [5, 10, 20, 50, 100];
  pageSizeValue: number = 10;

  get pages(): number[] {
    return Array.from(
      { length: this.props()?.totalPages ?? 0 },
      (_, i) => i + 1
    );
  }

  @Output() pageChange = new EventEmitter<number>();
  @Output() pageSizeChange = new EventEmitter<number>();

  ngOnInit() {
    this.pageSizeValue = this.props()?.pageSize ?? 10;
  }

  ngOnChanges() {
    this.pageSizeValue = this.props()?.pageSize ?? 10;
  }

  goToFirst() {
    if ((this.props()?.pageNumber ?? 1) > 1) {
      this.pageChange.emit(1);
    }
  }

  goToPrevious() {
    if ((this.props()?.pageNumber ?? 1) > 1) {
      this.pageChange.emit((this.props()?.pageNumber ?? 1) - 1);
    }
  }

  goToNext() {
    if ((this.props()?.pageNumber ?? 1) < (this.props()?.totalPages ?? 1)) {
      this.pageChange.emit((this.props()?.pageNumber ?? 1) + 1);
    }
  }

  goToLast() {
    if ((this.props()?.pageNumber ?? 1) < (this.props()?.totalPages ?? 1)) {
      this.pageChange.emit(this.props()?.totalPages ?? 1);
    }
  }

  onSelectPage(event: Event) {
    const value = Number((event.target as HTMLSelectElement).value);
    if (value !== (this.props()?.pageNumber ?? 1)) {
      this.pageChange.emit(value);
    }
  }

  onPageSizeChangeModel(value: number) {
    if (value !== (this.props()?.pageSize ?? 10)) {
      this.pageSizeChange.emit(value);
    }
  }
}
