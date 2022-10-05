import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CompanyComponent } from './components/company/company.component';
const routes:Routes =
[
  {path:'',component:HomeComponent},
  {path:'company',component:CompanyComponent},

]
@NgModule({
    imports: [RouterModule.forChild(routes)],
      exports: [RouterModule]
  })

export class InvoiceRoutingModule {}
