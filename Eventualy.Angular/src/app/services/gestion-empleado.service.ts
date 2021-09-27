import { Injectable } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import{HttpClient} from '@angular/common/http';
import { Empleado } from '../model/empleado';


@Injectable({
  providedIn: 'root'
})
export class GestionEmpleadoService {
  constructor(private http:HttpClient) { }
  readonly rootURL = 'https://localhost:44311/api';
  empleado:Empleado;
  listaEmpleado:Empleado[];

  refrescarListaEmpleado(){
    this.http.get(this.rootURL + '/Clientes')
    .toPromise()
    .then(res=> this.listaEmpleado = res as Empleado[])
  }


  guardarEmpleado(){
    
    if(this.empleado.ClienteId==null){
      this.empleado.ClienteId=0;
      
    }
    this.empleado.Estado=true;

    return this.http.post(this.rootURL + '/clientes', this.empleado);   
  } 
 
}
