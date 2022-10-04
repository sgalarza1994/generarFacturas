import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../core/services/http-service.service';
import { IInvoiceRequest } from '../models/home/invoice.interface';
import { IResponse, Response } from '../models/generic/response.interface';
import { IInvoiceResponse } from '../models/home/invoiceresponse.interface';
@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  constructor(private http:HttpService) { }

  addInvoice(request:IInvoiceRequest) :Observable<Response>
  {
    return this.http.post('invoice/addInvoice',request);
  }
  getInvoice(userId:number,isAdmin:boolean) :Observable<IResponse<IInvoiceResponse[]>>
  {
    let url = `invoice/getInvoices/${userId}/${isAdmin}`;
    return this.http.get(url);
  }

}
