import { Gadget } from '@/models/Gadget';
import { Pagination } from '@/models/Pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

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
    let params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);

    if (filterString) {
      params = params.set('filterString', filterString);
    }

    return this.http.get<Pagination>(this.API_URL, { params }).pipe(
      catchError((error) => {
        console.error('Error al obtener gadgets:', error);
        return throwError(() => error);
      })
    );
  }

  getGadgetById(id: number): Observable<Gadget> {
    const params = new HttpParams().set('id', id);

    return this.http.get<Gadget>(this.API_URL, { params }).pipe(
      catchError((error) => {
        console.error(`Error al obtener gadget con id ${id}:`, error);
        return throwError(() => error);
      })
    );
  }
}
