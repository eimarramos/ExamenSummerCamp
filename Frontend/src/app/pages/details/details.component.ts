import { Gadget } from '@/models/Gadget';
import { GadgetService } from '@/services/gadget-service.service';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonModule, DatePipe, CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-details',
  standalone: true,
  imports: [CommonModule, DatePipe, CurrencyPipe],
  templateUrl: './details.component.html',
  styleUrl: './details.component.css',
})
export class DetailsComponent implements OnInit {
  private gadgetService = inject(GadgetService);

  route = inject(ActivatedRoute);
  router = inject(Router);

  curso?: Observable<Gadget | undefined>;

  ngOnInit() {
    const cursoIdParam = this.route.snapshot.params['id'];

    const cursoId = Number(cursoIdParam);
    this.curso = this.gadgetService.getGadgetById(cursoId);
  }
}
