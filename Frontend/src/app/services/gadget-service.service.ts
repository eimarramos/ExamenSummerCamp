import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GadgetService {
  private apiUrl = 'https://localhost:7298/api/gadgets';

  constructor(private http: HttpClient) {}

  getGadgets(
    pageNumber: number,
    pageSize: number
  ): Observable<GadgetsResponse> {
    const url = `${this.apiUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}`;
    return this.http.get<GadgetsResponse>(url);
  }
}

export interface Gadget {
  id: number;
  name: string;
  brand: string;
  category: string;
  releaseDate: string;
  price: number;
  isAvailable: boolean;
}

export interface GadgetsResponse {
  items: Gadget[];
  pageNumber: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}
