import { Component, inject, signal } from '@angular/core';
import { Gadget, GadgetService } from '@/services/gadget-service.service';
import { TableComponent } from './components/table/table.component';
import { PaginationComponent } from './components/pagination/pagination.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [TableComponent, PaginationComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  private gadgetService = inject(GadgetService);
  readonly gadgets = signal<Gadget[]>([]);

  pageNumber = signal(0);
  totalPages = signal(0);
  totalCount = signal(0);
  hasPreviousPage = signal(false);
  hasNextPage = signal(false);

  constructor() {
    this.loadGadgets(1);
  }

  onPageChange(page: number) {
    this.loadGadgets(page);
  }

  private loadGadgets(page: number) {
    this.gadgetService.getGadgets(page, 10).subscribe((pagination) => {
      this.gadgets.set(pagination.items);
      this.pageNumber.set(pagination.pageNumber);
      this.totalPages.set(pagination.totalPages);
      this.totalCount.set(pagination.totalCount);
      this.hasPreviousPage.set(pagination.hasPreviousPage);
      this.hasNextPage.set(pagination.hasNextPage);
    });
  }
}
