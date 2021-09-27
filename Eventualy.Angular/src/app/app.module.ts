import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GestionEmpleadosComponent } from './components/gestion-empleados/gestion-empleados.component';
import { RegistrarEmpleadoComponent } from './components/gestion-empleados/registrar-empleado/registrar-empleado.component';
import { ListarEmpleadoComponent } from './components/gestion-empleados/listar-empleado/listar-empleado.component';
import{FormsModule, ReactiveFormsModule} from "@angular/forms";
import { GestionEmpleadoService } from './services/gestion-empleado.service';
import{HttpClientModule} from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    GestionEmpleadosComponent,
    RegistrarEmpleadoComponent,
    ListarEmpleadoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule

  ],
  providers: [GestionEmpleadoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
