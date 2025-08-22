import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search-bar',
  standalone: true,
  imports: [],
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.css',
})
export class SearchBarComponent {
  @Output() onSearch = new EventEmitter<string>();
  value: string = '';

  onInput(event: Event) {
    const input = event.target as HTMLInputElement;
    this.value = input.value;
  }

  send() {
    this.onSearch.emit(this.value);
  }
}
