import { Component, OnInit } from '@angular/core';
import {FormBuilder, Validators,FormGroup} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LoaderService } from 'src/app/core/services/loader.service';
import { IRegisterUser } from 'src/app/models/auth/registerUser.interface';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.scss']
})
export class RegistroComponent implements OnInit {

  formRegistro:FormGroup = new FormGroup({});
  formUsuario:FormGroup = new FormGroup({});
  constructor(private fb:FormBuilder,private authService:AuthService,
    private loaderService:LoaderService,
    private toashService:ToastrService,
    private router:Router
    ) { }

  ngOnInit(): void {

    this.formRegistro = this.fb.group({
       firstName:['',Validators.compose([Validators.required])],
       lastName:['',Validators.compose([Validators.required])],
       phone:[''],
       address : [''],
    });

    this.formUsuario = this.fb.group({
      email:['',Validators.compose([Validators.required])],
      password:['',Validators.compose([Validators.required])],
   });

  }


  registrarUsuario()
  {
    if(this.formRegistro.valid && this.formUsuario.valid)
    {
      let requestUser:IRegisterUser =
      {
        address : this.formRegistro.value.address,
        phone : this.formRegistro.value.phone,
        firstName : this.formRegistro.value.firstName,
        lastName : this.formRegistro.value.lastName,
        email : this.formUsuario.value.email,
        password : this.formUsuario.value.password

      };
      this.loaderService.isLoading.next(true);
      this.authService.registerUser(requestUser).subscribe({
        next : (response) =>{
          if(response)
          {
            if(response.success)
            {
               this.router.navigate(['auth']);
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
        complete :() =>
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

  get email()
  {
    return this.formUsuario.get('email');
  }

  get firstName()
  {
    return this.formRegistro.get('firstName');
  }

  get lastName()
  {
    return this.formRegistro.get('lastName');
  }

   get phone()
  {
    return this.formRegistro.get('phone');
  }
  get address()
  {
    return this.formRegistro.get('address');
  }
  get password()
  {
    return this.formUsuario.get('password');
  }
}
