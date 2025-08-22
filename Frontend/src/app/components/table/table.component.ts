import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { Component, input } from '@angular/core';
import { Gadget } from '@/services/gadget-service.service';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [DatePipe, CurrencyPipe, CommonModule],
  templateUrl: './table.component.html',
  styleUrl: './table.component.css',
})
export class TableComponent {
  gadgets = input<Gadget[]>([]);
}
