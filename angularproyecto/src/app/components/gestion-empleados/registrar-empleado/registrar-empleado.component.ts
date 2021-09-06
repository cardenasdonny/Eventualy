import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GestionEmpleadoService } from 'src/app/services/gestion-empleado.service';

@Component({
  selector: 'app-registrar-empleado',
  templateUrl: './registrar-empleado.component.html',
  styleUrls: ['./registrar-empleado.component.css']
})
export class RegistrarEmpleadoComponent implements OnInit {

  constructor(public gestionEmpleadoService:GestionEmpleadoService, private formBuilder:FormBuilder) { }
  formularioRegistroEmpleado:any;
  ngOnInit() {
    this.formularioRegistroEmpleado = this.formBuilder.group({
      ClienteId:[0],
      Nombres:["", [Validators.required]]
    });

  }

  onSubmit(){
    console.log(this.formularioRegistroEmpleado.controls["Nombre"].value);
  }



}
