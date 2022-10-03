import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/core/services/loader.service';
import { TokenService } from 'src/app/core/services/token.service';
import { ILoginUser } from 'src/app/models/auth/loginUser.interface';
import { AuthService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  mostrarPassword:boolean=false;

  formLogin:FormGroup = new FormGroup({});



  constructor(private fb:FormBuilder,
    private authService:AuthService,
    private loaderService:LoaderService,
    private toashService:ToastrService,
    private tokenService:TokenService
    ) { }

  ngOnInit(): void {

    this.formLogin = this.fb.group({
       email : ['',Validators.compose([Validators.required,Validators.email])],
       password: ['',Validators.compose([Validators.required])],
    });

  }
 iniciarSesion()
 {

  if(this.formLogin.valid)
  {
    let userRequest:ILoginUser =
    {
      password : this.formLogin.value.password,
      email : this.formLogin.value.email
    };
    this.loaderService.isLoading.next(true);
    this.authService.loginUser(userRequest).subscribe({
      next : (response) =>
      {
        if(response)
        {
          if(response.success)
          {
            this.toashService.success("Vamos a iniciar sesion s");
            this.tokenService.saveAuthorize(response.result);
          }
          else
          {
            this.toashService.error(response.message);
          }
        }
      },
      error : (error:any) =>
      {

      },
      complete : () =>
      {
        this.loaderService.isLoading.next(false);
      }
    })
  }
  else
  {
    this.toashService.error("Formulario no es valido");
  }

 }


 get password()
 {
   return this.formLogin.get('password');
 }
 get email()
 {
   return this.formLogin.get('email');
 }
}
