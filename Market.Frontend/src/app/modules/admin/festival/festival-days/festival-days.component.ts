import { Component, inject, OnInit } from '@angular/core';
import { BaseListPagedComponent } from '../../../../core/components/base-classes/base-list-paged-component';
import { FestivalDaysApiService } from '../../../../api-services/festival-days/festival-days-api.service';
import {
  ListFestivalDaysRequest,
  ListFestivalDaysQueryDto,
} from '../../../../api-services/festival-days/festival-days-api.model';
import { MatDialog } from '@angular/material/dialog';
import { ToasterService } from '../../../../core/services/toaster.service';
import { FestivalDayUpsertComponent } from './festival-day-upsert/festival-day-upsert.component';
import { DialogHelperService } from '../../../shared/services/dialog-helper.service';
import { DialogButton } from '../../../shared/models/dialog-config.model';

@Component({
  selector: 'app-festival-days',
  standalone: false,
  templateUrl: './festival-days.component.html',
  styleUrl: './festival-days.component.scss',
})
export class FestivalDaysComponent
  extends BaseListPagedComponent<ListFestivalDaysQueryDto, ListFestivalDaysRequest>
  implements OnInit {
  private api = inject(FestivalDaysApiService);
  private dialog = inject(MatDialog);
  private toaster = inject(ToasterService);
  private dialogHelper = inject(DialogHelperService);
  displayedColumns: string[] = ['date', 'opensAt', 'capacity', 'program', 'actions'];

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
      error: (err) => {
        this.stopLoading('Greska pri ucitavanju dana festivala');
        console.error('Load festival days error:', err);
      }
    });
  }

  onCreate(): void {
    const dialogRef = this.dialog.open(FestivalDayUpsertComponent, {
      width: '500px',
      maxWidth: '90vw',
      data: { mode: 'create' },
    });

    dialogRef.afterClosed().subscribe((success: boolean) => {
      if (success) {
        this.toaster.success('Dan festivala je dodan.');
        this.loadPagedData();
      }
    });
  }


  onEdit(day: ListFestivalDaysQueryDto): void {
    const dialogRef = this.dialog.open(FestivalDayUpsertComponent, {
      width: '500px',
      maxWidth: '90vw',
      data: { mode: 'edit', festivalDayId: day.id },
    });

    dialogRef.afterClosed().subscribe((success: boolean) => {
      if (success) {
        this.toaster.success('Dan festivala je izmijenjen.');
        this.loadPagedData();
      }
    });
  }

  private performDelete(day: ListFestivalDaysQueryDto): void {
    this.startLoading();

    this.api.delete(day.id).subscribe({

      next: (response) => {
        this.toaster.success('Dan festivala je obrisan.');
        this.loadPagedData();
      },
      error: (response) => {
        this.stopLoading();
        this.toaster.error(response.error?.message ?? 'Greska pri brisanju.');
      }


    })
  }

  onDelete(day: ListFestivalDaysQueryDto): void {
    this.dialogHelper.confirmDelete(`Dan ${day.date}`).subscribe((result) => {
      if (result && result.button === DialogButton.DELETE) {
        this.performDelete(day);
      }
    });
  }

}