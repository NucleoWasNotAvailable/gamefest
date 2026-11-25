import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup } from '@angular/forms';
import { FestivalDaysApiService } from '../../../../../api-services/festival-days/festival-days-api.service';
import {
  CreateFestivalDayCommand,
  UpdateFestivalDayCommand,
} from '../../../../../api-services/festival-days/festival-days-api.model';
import { FestivalDayForm, FestivalDayFormService } from '../services/festival-day-form.service';
import { applyServerErrors } from '../../../../../core/forms/server-errors';

/** What the list sends when it opens the dialog. */
export interface FestivalDayDialogData {
  mode: 'create' | 'edit';
  festivalDayId?: number;
}

@Component({
  selector: 'app-festival-day-upsert',
  standalone: false,
  templateUrl: './festival-day-upsert.component.html',
  styleUrl: './festival-day-upsert.component.scss',
  providers: [FestivalDayFormService],
})
export class FestivalDayUpsertComponent implements OnInit {
  private dialogRef = inject(MatDialogRef<FestivalDayUpsertComponent>);
  private data = inject<FestivalDayDialogData>(MAT_DIALOG_DATA);
  private api = inject(FestivalDaysApiService);
  protected formService = inject(FestivalDayFormService);
  private destroyRef = inject(DestroyRef);

  form?: FormGroup<FestivalDayForm>;
  isLoading = false;
  errorMessage: string | null = null;
  isEditMode = false;

  ngOnInit(): void {
    this.isEditMode = this.data.mode === 'edit';

    if (this.isEditMode && this.data.festivalDayId) {
      this.loadFestivalDay(this.data.festivalDayId);
    } else {
      this.form = this.formService.createFestivalDayForm();
    }
  }

  private loadFestivalDay(id: number): void {
    this.isLoading = true;

    this.api.getById(id).pipe(takeUntilDestroyed(this.destroyRef)).subscribe({
      next: (day) => {
        this.form = this.formService.createFestivalDayForm(day);
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Load festival day error:', err);
        this.dialogRef.close(false);
      },
    });
  }

  onSubmit(): void {
    if (!this.form || this.isLoading) return;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;

    if (this.isEditMode && this.data.festivalDayId) {
      this.updateFestivalDay(this.form);
    } else {
      this.createFestivalDay(this.form);
    }
  }

  private createFestivalDay(form: FormGroup<FestivalDayForm>): void {
    const command: CreateFestivalDayCommand = {
      date : form.controls.date.value,
      opensAt : form.controls.opensAt.value,
      program:form.controls.program.value.trim(),
      capacity:form.controls.capacity.value,
    };

    this.api.create(command).subscribe({
      next: () => this.dialogRef.close(true),
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = applyServerErrors(form, err);
        console.error('Create festival day error:', err);
      },
    });
  }

  private updateFestivalDay(form: FormGroup<FestivalDayForm>): void {
    const command: UpdateFestivalDayCommand = {
     date : form.controls.date.value,
      opensAt : form.controls.opensAt.value,
      program:form.controls.program.value.trim(),
      capacity:form.controls.capacity.value,
    };

    this.api.update(this.data.festivalDayId!, command).subscribe({
      next: () => this.dialogRef.close(true),
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = applyServerErrors(form, err);
        console.error('Update festival day error:', err);
      },
    });
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}