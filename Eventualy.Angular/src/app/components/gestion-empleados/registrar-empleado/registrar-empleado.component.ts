import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-registrar-empleado',
  templateUrl: './registrar-empleado.component.html',
  styleUrls: ['./registrar-empleado.component.css']
})
export class RegistrarEmpleadoComponent implements OnInit {


  constructor(private formBuilder:FormBuilder) { }
  formularioRegistroEmpleado = this.formBuilder.group({
    ClienteId:[0],
    Nombres:["", [Validators.required]]
  });

  ngOnInit(): void {
  }
  onSubmit(){
    console.log(this.formularioRegistroEmpleado.controls["Nombres"].value);
  }


}
