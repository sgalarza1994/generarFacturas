import { Component, OnInit,OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IUser } from 'src/app/models/auth/user.interface';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit,OnDestroy {
  //@ts-ignore
  suscripcionUsuario: Subscription;
  user:IUser | undefined;
  userName:string="";
  constructor(private router:Router,
    private authService:AuthService
    ) { }

  ngOnInit(): void {
    let usuario$ = this.authService.usuarioAuth$
    this.suscripcionUsuario = usuario$.subscribe((user:IUser) => {
      if(user != null && user !== undefined)
      {
        this.userName = user.fullName;
        this.user = user;
      }

    });


    if(this.userName === "")
    {
      this.router.navigate(['auth']);
    }
    else
    {
      this.router.navigate(["invoice"]);
    }
  }

  ngOnDestroy(): void {
    this.suscripcionUsuario.unsubscribe();
  }
  navegacion()
  {
    this.router.navigate(["invoice/company"]);
  }

}
