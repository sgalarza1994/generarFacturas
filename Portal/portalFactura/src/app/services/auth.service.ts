import { Injectable } from '@angular/core';
import { HttpService } from '../core/services/http-service.service';
import { IRegisterUser } from '../models/auth/registerUser.interface';
import { BehaviorSubject, observable, Observable, of } from 'rxjs';
import { IResponse, Response } from '../models/generic/response.interface';
import { ILoginUser } from '../models/auth/loginUser.interface';
import { IUser } from '../models/auth/user.interface';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpService) { }



  registerUser(request:IRegisterUser) : Observable<Response>
  {
    return this.http.post('user/createUser',request);
  }

  loginUser(request:ILoginUser) : Observable<IResponse<IUser>>
  {
    return this.http.post('user/loginUser',request);
  }
}
