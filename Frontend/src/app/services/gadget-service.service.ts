import { Gadget } from '@/models/Gadget';
import { Pagination } from '@/models/Pagination';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GadgetService {
  private readonly API_URL = 'https://localhost:7298/api/gadgets';

  constructor(private http: HttpClient) {}

  getGadgets(
    pageNumber: number,
    pageSize: number,
    filterString: string = ''
  ): Observable<Pagination> {
    const url =
      `${this.API_URL}?pageNumber=${pageNumber}&pageSize=${pageSize}` +
      (filterString ? `&filterString=${encodeURIComponent(filterString)}` : '');

    return this.http.get<Pagination>(url);
  }

  getGadgetById(id: number): Observable<Gadget> {
    const url = `${this.API_URL}/${id}`;

    return this.http.get<Gadget>(url);
  }
}
