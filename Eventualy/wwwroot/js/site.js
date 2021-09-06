$(document).ready(function () {

    cambiarEstado = (url) => {
        /*alert(url);*/
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                alertify.set('notifier', 'position', 'top-right');
                if (res.isValid) {
                    location.reload();

                    //alertify.notify("Cambio de estado exitoso", 'success', 0, function () {

                    //    location.reload();
                    //})

                } else {
                    if (res.tipoError == "warning")
                        alertify.alert('Cambiar estado', '¡No se puede cambiar el estado del usuario actual!', function () { location.reload(); });
                    else
                        alertify.error("Error al cambiar el estado");
                }
            }
        })
    }






    mostrarModal = (url, title) => {
        //alert("Ingresa a la función");
        //alert(url);
        /*alert(title);*/
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                if (res.isValid == false) {
                    alertify.set('notifier', 'position', 'top-right');
                    if (res.tipoError == "error")
                        alertify.error(res.mensaje);
                } else {
                    $('#form-modal .modal-body').html(res);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');
                }
            }
        })
        //to prevent default form submit event
        return false;
    }



    jQueryAjaxPost = (form, titulo, mensaje) => {
        //alert(form);
        //alert(titulo);
        //alert(mensaje);
        alertify.confirm(titulo, mensaje,
            function () {
                try {
                    $.ajax({
                        type: 'POST',
                        url: form.action,
                        data: new FormData(form),
                        contentType: false,
                        processData: false,
                        success: function (res) {
                            if (res.isValid) {
                                var mensaje;
                                if (res.operacion == "crear") {
                                    mensaje = "Registro creado exitosamente";
                                }
                                if (res.operacion == "ok") {
                                    mensaje = res.mensaje;
                                }

                                if (res.operacion == "editar") {
                                    mensaje = "Registro editado exitosamente";
                                }

                                alertify.set('notifier', 'position', 'top-right');
                                alertify.notify(mensaje, 'success', 3, function () {
                                    $('#form-modal .modal-body').html('');
                                    $('#form-modal .modal-title').html('');
                                    $('#form-modal').modal('hide');
                                    location.reload();
                                })
                            }
                            else {
                                alertify.set('notifier', 'position', 'top-right');
                                if (res.tipoError == "error")
                                    alertify.error(res.error);
                                else {
                                    alertify.warning(res.error);
                                    $('#form-modal .modal-body').html(res.html);
                                }
                            }
                        },
                        error: function (err) {
                            console.log(err)
                        }
                    })
                } catch (ex) {
                    console.log(ex)
                }
            },
            function () {
                alertify.error('Cancelado');
            }).set('labels', { ok: 'Guardar', cancel: 'Cancelar' }).set('notifier', 'position', 'top-right');


        //to prevent default form submit event
        return false;
    }

});
