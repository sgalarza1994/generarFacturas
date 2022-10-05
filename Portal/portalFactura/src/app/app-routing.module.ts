import { NgModule } from '@angular/core';
import { RouterModule, Routes,PreloadAllModules } from '@angular/router';
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
    },
    {
      path: 'invoice',
      loadChildren:() =>import('./invoice/invoice.module').then(x=>x.InvoiceModule)
    }
  ]
 }


];

@NgModule({
  imports: [RouterModule.forRoot(routes,{
    preloadingStrategy: PreloadAllModules
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
