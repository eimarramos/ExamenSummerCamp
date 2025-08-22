import { Gadget } from '@/models/Gadget';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { Component, input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [DatePipe, CurrencyPipe, CommonModule],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css',
})
export class TableComponent {
  gadgets = input<Gadget[]>([]);

  @Output() rowClick = new EventEmitter<Gadget>();

  onRowClick(gadget: Gadget) {
    this.rowClick.emit(gadget);
  }
}
