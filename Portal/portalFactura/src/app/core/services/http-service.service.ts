import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpHeaders, HttpParams, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private readonly HEADERS_FORM_DATA = {
    'enctype': 'multipart/form-data'
};

private readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }



  constructor(private httpClient: HttpClient) { }


  public post<T>(endPoint: string, body: any): Observable<T> {
    return this.httpClient.post<T>(this.buildUrl(endPoint), body, this.httpOptions);
  }

  public get<T>(
    endPoint: string,
    httpParams?: HttpParams | { [param: string]: string | string[] })
    : Observable<T> {
    return this.httpClient.get<T>(this.buildUrl(endPoint), { params: httpParams });
  }


  private buildUrl(endPoint: string): string {
    return `${environment.API_BASE_URL}${endPoint}`;

  }


}
