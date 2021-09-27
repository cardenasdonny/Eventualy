import { Component, OnInit } from '@angular/core';
import { Empleado } from 'src/app/model/empleado';
import { GestionEmpleadoService } from 'src/app/services/gestion-empleado.service';

@Component({
  selector: 'app-listar-empleado',
  templateUrl: './listar-empleado.component.html',
  styleUrls: ['./listar-empleado.component.css']
})
export class ListarEmpleadoComponent implements OnInit {

  constructor(public gestionEmpleadoService:GestionEmpleadoService) { }
  llenarFormularioEmpleado(empleado:Empleado){
    
    //this.gestionEmpleadoService.formularioRegistroEmpleado.patchValue(empleado);
  }


  ngOnInit(): void {
    this.gestionEmpleadoService.refrescarListaEmpleado();
  }

}
