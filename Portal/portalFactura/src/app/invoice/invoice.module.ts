import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './components/home/home.component';
import { AppMaterialModules } from '../material/material.module';
import { InvoiceRoutingModule } from './invoice-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    InvoiceRoutingModule,
    AppMaterialModules,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class InvoiceModule { }
