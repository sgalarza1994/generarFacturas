import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { AuthRoutingModule } from './auth-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegistroComponent } from './components/registro/registro.component';
import { AppMaterialModules } from '../material/material.module';

@NgModule({
  declarations: [
    LoginComponent,
    RegistroComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AuthRoutingModule,
    AppMaterialModules
  ]
})
export class AuthModule { }
