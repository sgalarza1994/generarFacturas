import { Injectable } from '@angular/core';
import { HttpService } from '../core/services/http-service.service';
import { IRegisterUser } from '../models/auth/registerUser.interface';
import { BehaviorSubject, observable, Observable, of } from 'rxjs';
import { IResponse, Response } from '../models/generic/response.interface';
import { ILoginUser } from '../models/auth/loginUser.interface';
import { IUser } from '../models/auth/user.interface';
import { TokenService } from '../core/services/token.service';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private usuarioAuth = new BehaviorSubject<any>(null);
  usuarioAuth$ = this.usuarioAuth.asObservable();

  constructor(private http:HttpService,
    private tokenService:TokenService
    ) { 

       this.notificarLogin();

    }



  registerUser(request:IRegisterUser) : Observable<Response>
  {
    return this.http.post('user/createUser',request);
  }

  loginUser(request:ILoginUser) : Observable<IResponse<IUser>>
  {
    return this.http.post('user/loginUser',request);
  }

  public notificarLogin () 
  {
    this.usuarioAuth.next(this.tokenService.getUser());
  }
}
