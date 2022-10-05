import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from '../core/services/http-service.service';
import { IResponse,Response } from '../models/generic/response.interface';
import { IInvoiceDetail } from '../models/home/invoiceDetail.interface';
@Injectable({
  providedIn: 'root'
})
export class InvoiceDetailService {

  constructor(private http:HttpService) { }

  addInvoiceDetail(request:IInvoiceDetail[]) :Observable<Response>
  {
    return this.http.post('invoiceDetail/addInvoiceDetail',request);
  }
  getInvoice(invoiceId:number) :Observable<IResponse<IInvoiceDetail[]>>
  {
    let url = `invoiceDetail/getInvoices/${invoiceId}`;
    return this.http.get(url);
  }
}
