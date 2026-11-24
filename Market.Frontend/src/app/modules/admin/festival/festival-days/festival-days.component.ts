import { Component, inject, OnInit } from '@angular/core';
import { BaseListPagedComponent } from '../../../../core/components/base-classes/base-list-paged-component';
import { FestivalDaysApiService } from '../../../../api-services/festival-days/festival-days-api.service';
import {
  ListFestivalDaysRequest,
  ListFestivalDaysQueryDto,
} from '../../../../api-services/festival-days/festival-days-api.model';

@Component({
  selector: 'app-festival-days',
  standalone: false,
  templateUrl: './festival-days.component.html',
  styleUrl: './festival-days.component.scss',
})
export class FestivalDaysComponent
  extends BaseListPagedComponent<ListFestivalDaysQueryDto, ListFestivalDaysRequest>
  implements OnInit
{
  private api = inject(FestivalDaysApiService);                     

  displayedColumns: string[] = ['date', 'opensAt', 'capacity', 'program'];            

  constructor() {
    super();
    this.request = new ListFestivalDaysRequest();                   
  }

  ngOnInit(): void {
    this.initList();
  }

  protected loadPagedData(): void {
    this.startLoading();
    this.api.list(this.request).subscribe({
      next: (response) => {
        this.handlePageResult(response);
        this.stopLoading();
      },
      error: (err) =>{
        this.stopLoading('Greska pri ucitavanju dana festivala');
        console.error('Load festival days error:', err);
      }
    });
  }
}