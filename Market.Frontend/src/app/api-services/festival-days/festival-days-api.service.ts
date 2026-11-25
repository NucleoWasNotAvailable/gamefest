import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { buildHttpParams } from '../../core/models/build-http-params';
import { CreatedResponse } from '../../core/models/created-response';
import {
    ListFestivalDaysRequest,
    ListFestivalDaysResponse,
    GetFestivalDayByIdQueryDto,
    CreateFestivalDayCommand,
    UpdateFestivalDayCommand,
} from './festival-days-api.model';

@Injectable({ providedIn: 'root' })
export class FestivalDaysApiService {
    private readonly baseUrl = `${environment.apiUrl}/FestivalDays`;
    private http = inject(HttpClient);


    list(request?: ListFestivalDaysRequest): Observable<ListFestivalDaysResponse> {
        const params = request ? buildHttpParams(request as any) : undefined;
        return this.http.get<ListFestivalDaysResponse>(this.baseUrl, { params });
    }



    getById(id: number): Observable<GetFestivalDayByIdQueryDto> {
        return this.http.get<GetFestivalDayByIdQueryDto>(`${this.baseUrl}/${id}`);
    }

    create(payload: CreateFestivalDayCommand): Observable<CreatedResponse> {
        return this.http.post<CreatedResponse>(this.baseUrl, payload);
    }

    update(id: number, payload: UpdateFestivalDayCommand): Observable<void> {
        return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }
}