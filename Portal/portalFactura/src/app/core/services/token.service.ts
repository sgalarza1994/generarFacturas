import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { IUser } from 'src/app/models/auth/user.interface';
import { environment } from '../../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class TokenService {

  private secret = environment.SECRE;
  constructor() { }

  saveAuthorize(response:IUser)
  {
      let user = JSON.stringify(response);
      let userCifrado:any = CryptoJS.AES.encrypt(user,this.secret);
      localStorage.setItem("user",userCifrado);
  }

  getUser():IUser
  {
      let user:IUser = {companyId:0,fullName:'',rolId:0,rolName:'',token:'',userId:0};
        let auth = localStorage.getItem('user');
        if(auth)
        {
            if(auth !== null)
            {
              let bytes = CryptoJS.AES.decrypt(auth, this.secret);
              user = JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
            }
        }
      return user;
    }
  removeAuthorize()
  {

    localStorage.removeItem("token");
    localStorage.removeItem("user");
  }

}
