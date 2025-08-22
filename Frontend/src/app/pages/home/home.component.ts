import { Gadget } from '@/models/Gadget';
import { GadgetService } from '@/services/gadget-service.service';
import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { TableComponent } from '../../components/table/table.component';
import { PaginationComponent } from '../../components/pagination/pagination.component';
import { SearchBarComponent } from '@/components/search-bar/search-bar.component';
import type { PaginationProps } from '@/components/pagination/pagination.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [TableComponent, PaginationComponent, SearchBarComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  private gadgetService = inject(GadgetService);
  private router = inject(Router);
  readonly gadgets = signal<Gadget[]>([]);

  pageSize = signal(10);
  pageNumber = signal(1);
  totalPages = signal(0);
  totalCount = signal(0);
  hasPreviousPage = signal(false);
  hasNextPage = signal(false);

  private currentFilter: string = '';

  constructor() {
    this.loadGadgets(1);
  }

  onPageChange(page: number) {
    this.loadGadgets(page, this.pageSize(), this.currentFilter);
  }

  private loadGadgets(
    page: number,
    pageSize: number = this.pageSize(),
    filter: string = this.currentFilter
  ) {
    this.gadgetService
      .getGadgets(page, pageSize, filter)
      .subscribe((result) => {
        this.updateValues(result);
      });
  }
  onPageSizeChange(size: number) {
    this.pageSize.set(size);
    this.loadGadgets(1, size, this.currentFilter);
  }

  onRowClick($event: Gadget) {
    if ($event?.id) {
      this.router.navigate([`/details/${$event.id}`]);
    }
  }

  onSearch(filterString: string) {
    this.currentFilter = filterString;
    this.loadGadgets(1, this.pageSize(), filterString);
  }

  updateValues(pagination: PaginationProps & { items: Gadget[] }) {
    this.gadgets.set(pagination.items);
    this.pageNumber.set(pagination.pageNumber);
    this.totalPages.set(pagination.totalPages);
    this.totalCount.set(pagination.totalCount);
    this.hasPreviousPage.set(pagination.hasPreviousPage);
    this.hasNextPage.set(pagination.hasNextPage);
  }
}
