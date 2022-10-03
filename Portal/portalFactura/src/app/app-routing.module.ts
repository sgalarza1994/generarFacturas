import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './layout/home/home.component';
const routes: Routes = [

 {
  path : '',
  component : HomeComponent,
  children:
  [
    {
      path: 'auth',
      loadChildren:() =>import('./auth/auth.module').then(x=>x.AuthModule)
    }
  ]
 }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
