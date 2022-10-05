import { Injectable } from '@angular/core';
import { IResponse, Response } from '../models/generic/response.interface';
import { Observable } from 'rxjs';
import { HttpService } from '../core/services/http-service.service';
import { ICompany } from '../models/home/company.interface';
@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private http:HttpService) { }


  getCompany(userId:number,isAdmin:boolean) :Observable<IResponse<ICompany[]>>
  {
    let url = `company/GetCompany/${userId}/${isAdmin}`;
    return this.http.get(url);
  }

  addCompany(request:ICompany) :Observable<Response>
  {

    return this.http.post('company/addCompany',request);
  }
}
