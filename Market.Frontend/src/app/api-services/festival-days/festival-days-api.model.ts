import { BasePagedQuery } from '../../core/models/paging/base-paged-query';
import { PageResult } from '../../core/models/paging/page-result';

//queries
export class ListFestivalDaysRequest extends BasePagedQuery
{
    dateFrom?: string | null;
    dateTo? : string | null;
    search? : string | null;
}

export interface ListFestivalDaysQueryDto 
{
    id: number;
    date: string;
    opensAt:string;
    capacity:number;
    program: string;
}

export type ListFestivalDaysResponse = PageResult<ListFestivalDaysQueryDto>;


export interface GetFestivalDayByIdQueryDto
{
    id:number;
    date:string;
    opensAt:string;
    capacity:number;
    program:string;

}

// commands


export interface CreateFestivalDayCommand
{
    date:string;
    opensAt:string;
    capacity:number;
    program:string;
}

export interface UpdateFestivalDayCommand
{
    date:string;
    opensAt:string;
    capacity:number;
    program:string;
}


export interface CreateFestivalDayCommand {
  date: string;
  opensAt: string;
  capacity: number;
  program: string;
}


export interface UpdateFestivalDayCommand {
  date: string;
  opensAt: string;
  capacity: number;
  program: string;
}