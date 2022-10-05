import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './components/home/home.component';
import { AppMaterialModules } from '../material/material.module';
import { InvoiceRoutingModule } from './invoice-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CompanyComponent } from './components/company/company.component';
import { PdfViewerModule } from 'ng2-pdf-viewer';
@NgModule({
  declarations: [
    HomeComponent,
    CompanyComponent
  ],
  imports: [
    CommonModule,
    InvoiceRoutingModule,
    AppMaterialModules,
    FormsModule,
    ReactiveFormsModule,
    PdfViewerModule
  ]
})
export class InvoiceModule { }
