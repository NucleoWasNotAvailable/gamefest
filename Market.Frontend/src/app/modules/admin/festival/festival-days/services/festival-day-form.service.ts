import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { GetFestivalDayByIdQueryDto } from '../../../../../api-services/festival-days/festival-days-api.model';


export interface FestivalDayForm {
    date: FormControl<string>;
    opensAt: FormControl<string>;
    capacity: FormControl<number>;
    program: FormControl<string>;
}

@Injectable()
export class FestivalDayFormService {

    readonly programMaxLength = 2000;
    readonly minCapacity = 1;
    readonly maxCapacity = 5000;

    createFestivalDayForm(day?: GetFestivalDayByIdQueryDto): FormGroup<FestivalDayForm> {
        return new FormGroup<FestivalDayForm>({
            date: new FormControl(day?.date ?? '', {
                nonNullable: true,
                validators: [Validators.required],
            }),

            opensAt: new FormControl(day?.opensAt ?? '',
                {
                    nonNullable: true,
                    validators: [Validators.required],
                }),

            capacity: new FormControl(day?.capacity ?? 1,
                {
                    nonNullable: true,
                    validators: [
                        Validators.required,
                        Validators.min(this.minCapacity),
                        Validators.max(this.maxCapacity),
                    ]
                }),

            program: new FormControl(day?.program ?? '',
                {
                    nonNullable: true,
                    validators: [
                        Validators.required,
                        Validators.maxLength(this.programMaxLength),
                    ]

                })
        });
    }
}
