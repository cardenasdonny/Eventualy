import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { GestionEmpleadoService } from 'src/app/services/gestion-empleado.service';
declare let alertify:any;
@Component({
  selector: 'app-registrar-empleado',
  templateUrl: './registrar-empleado.component.html',
  styleUrls: ['./registrar-empleado.component.css']
})
export class RegistrarEmpleadoComponent implements OnInit {
  constructor(private formBuilder:FormBuilder, public gestionEmpleadoService:GestionEmpleadoService ) { }

  exRegularLetras: any = "^[a-zA-Z ]*$";
  exRegularCorreo: any = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
  exRegularNumeros: any = "^[0-9]*$";

  formularioRegistroEmpleado = this.formBuilder.group({
    ClienteId:[0],
    Nombres:["", [Validators.required, Validators.pattern(this.exRegularLetras)]],
    Email:["", [Validators.required]] ,
    Documento:["", [Validators.required]] ,
    Estado:["", [Validators.required]] ,
    TipoDocumentoId:["", [Validators.required]],
    Edad:["", [Validators.required]],
    Cumpleanios:["", [Validators.required]]
  });

  get ClienteId(){
    return this.formularioRegistroEmpleado.controls["ClienteId"];
  }
  get Nombres(){
    return this.formularioRegistroEmpleado.controls["Nombres"];
  }
  get Email(){
    return this.formularioRegistroEmpleado.controls["Email"];
  }
  get Documento(){
    return this.formularioRegistroEmpleado.controls["Documento"];
  }
  get Estado(){
    return this.formularioRegistroEmpleado.controls["Estado"];
  }
  get TipoDocumentoId(){
    return this.formularioRegistroEmpleado.controls["TipoDocumentoId"];
  }
  get Edad(){
    return this.formularioRegistroEmpleado.controls["Edad"];
  } 
  get Cumpleanios(){
    return this.formularioRegistroEmpleado.controls["Cumpleanios"];
  } 

  ngOnInit(): void {
    
  }

  guarda(){
    alertify.success("Se registró el empleado");
  }

  onSubmit(){
    this.gestionEmpleadoService.empleado = this.formularioRegistroEmpleado.value;

     this.gestionEmpleadoService.guardarEmpleado().subscribe(
       res =>{
         this.formularioRegistroEmpleado.reset();
         console.log("Se registró el empleado");
         console.log(res);
         alertify.success("Se registró el empleado");

       },
       err =>{
         console.log(err);
       }
     )

  }

}
