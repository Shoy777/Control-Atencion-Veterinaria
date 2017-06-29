/*Validar Precio*/
+function ($) {
    $.validator.methods.number = function (value, element) {
        value = floatValue(value);
        return this.optional(element) || !isNaN(value);
    }
    $.validator.methods.range = function (value, element, param) {
        value = floatValue(value);
        return this.optional(element) || (value >= param[0] && value <= param[1]);
    }
    function floatValue(value) {
        return parseFloat(value.replace(",", "."));
    }
}(jQuery);

/*Selector de Fecha JQuery UI*/
+function ($) {
    $('.fecha').datepicker({
        changeMonth: true,
        changeYear: true,
        maxDate: 0,
        yearRange: "-30:+0",
        dateFormat: "dd/mm/yy"
    });
    $('.fechaAtencion').datepicker({
        changeMonth: true,
        changeYear: true,
        minDate:0,
        maxDate: 37,
        yearRange: "-0:+1",
        dateFormat: "dd/mm/yy"
    });
    
    $("#btnFecha").click(function () {
        $('.fecha').focus();
        $('.fechaAtencion').focus();
    });
    $.validator.addMethod('date', function (value, element, params) {
        if (this.optional(element)) {
            return true;
        }
        var ok = true;
        try {
            $.datepicker.parseDate('dd/mm/yy', value);
        }
        catch (err) {
            ok = false;
        }
        return ok;
    });
}(jQuery);

/*Mascotas*/
+function ($) {
    /*Escogiendo Especie - Listado de Razas por Especie*/
    $("#EspecieId").change(function () {
        $("#RazaId").empty();
        $.ajax({
            type: 'POST',
            url: '/Mascota/GetAllRazasByEspecie',
            dataType: 'json',
            data: { id: $("#EspecieId").val() },
            success: function (razas) {
                $("#RazaId").append('<option value="">' +
                     'Seleccione una raza' + '</option>');
                $.each(razas, function (i, raza) {
                    $("#RazaId").append('<option value="' + raza.Value + '">' +
                         raza.Text + '</option>');
                });
            },
            error: function (ex) {
                $("#RazaId").append('<option value="">' +
                     'Debe elegir una Especie' + '</option>');
            }
        });
        return false;
    });
    /*Registrando Cliente*/
    $("#frm-registrarCliente").submit(function () {
        var form = $(this);
        mostrar();
        form.ajaxSubmit({
            dataType: 'JSON',
            type: 'POST',
            url: '/Cliente/Registrar',
            data: form.serialize(),
            success: function (r) {
                setTimeout(ocultar, 1000);
                setTimeout((function () {
                    if (r.Message == "Registro guardado") {
                        $("#btnRegistrar").prop("disabled", true);
                        $("#MensajeError").text(r.Message).hide();
                        $("#NombreCliente").text(r.Nombre).hide();
                        $("#ApellidoCliente").text(r.Apellido).hide();
                        $("#TelefonoCliente").text(r.Telefono).hide();
                        $("#form-cliente").addClass("hide");
                        $("#MensajeExito").text(r.Message);
                        $("#ClienteGuardado").removeClass("hide");
                        $("#ClienteId").empty();
                        $.ajax({
                            type: 'POST',
                            url: '/Cliente/GetAllClientesByLast',
                            dataType: 'json',
                            success: function (clientes) {
                                $("#ClienteId").append('<option value="">' +
                                     'Seleccione un cliente' + '</option>');
                                $.each(clientes, function (i, cliente) {
                                    $("#ClienteId").append('<option value="' + cliente.Value + '">' +
                                         cliente.Text + '</option>');
                                });
                            },
                            error: function (ex) {
                                $("#ClienteId").append('<option value="">' +
                                     'Busque un Cliente' + '</option>');
                            }
                        });

                    }
                    else {
                        if (r.Nombre != "") {
                            $("#NombreCliente").text(r.Nombre).show();
                        } else {
                            $("#NombreCliente").text(r.archivo).hide();
                        }
                        if (r.Apellido != "") {
                            $("#ApellidoCliente").text(r.Apellido).show();
                        } else {
                            $("#ApellidoCliente").text(r.Apellido).hide();
                        }
                        if (r.Telefono != "") {
                            $("#TelefonoCliente").text(r.Telefono).show();
                        } else {
                            $("#TelefonoCliente").text(r.Telefono).hide();
                        }
                        if(r.Message == "Cliente Existe"){
                            $("#MensajeError").text(r.Message).show();
                        } else if(r.Message == ""){
                            $("#MensajeError").text(r.Message).hide();
                        }
                    }
                }), 1000);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
        return false;
    });

}(jQuery);

/*Consulta*/
+function ($) {
    /*Buscar Cliente x Nombre Completo*/
    $("#frm-buscarCliente").submit(function () {
        var form = $(this);
        mostrar();
        form.ajaxSubmit({
            dataType: 'JSON',
            type: 'POST',
            url: '/Cliente/GetClienteByQuery',
            data: form.serialize(),
            success: function (r) {
                setTimeout(ocultar, 1000);
                if (Object.keys(r).length > 0) {
                    $(".clientes").html("");
                    setTimeout((function () {
                        $.each(r, function (i, c) {
                            $(".clientes").append('<tr class="cliente"><td>' + c.ClienteId + '</td><td>' + c.Nombre + '</td><td>' + c.Apellido + '</td></tr>');
                        });
                    }), 1000);
                    $(".mensajeCliente").hide();
                } else {
                    $(".mensajeCliente").text("No se encontro ningun cliente").show();
                    $(".clientes").html("");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                setTimeout(ocultar, 1000);
                $(".mensajeCliente").text("Ha ocurrido un inconveniente").show();
            }
        });
        return false;
    });
    /*Seleccionando Cliente*/
    $('.clientes .cliente').on("click", function () {
        $(this).addClass("highlight").siblings().removeClass("highlight");
    });
    $(".clientes").on('click', '.cliente', function () {
        $(this).addClass("highlight").siblings().removeClass("highlight");
        var id = this.cells[0].textContent;
        var nomCompleto = this.cells[1].textContent +" "+ this.cells[2].textContent;
        $("#NombreCliente").val(nomCompleto);
        $("#ClienteId").val(id);
        $("#NombreMascota").val("");
        $("#MascotaId").val(0);
        GetMascotasByCliente(id);
    });
    /*Seleccionando Mascota*/
    $('.mascotas .mascota').on("click", function () {
        $(this).addClass("highlight").siblings().removeClass("highlight");
    });
    $(".mascotas").on('click', '.mascota', function () {
        $(this).addClass("highlight").siblings().removeClass("highlight");
        var id = this.cells[0].textContent;
        var nomCompleto = this.cells[1].textContent;
        $("#NombreMascota").val(nomCompleto);
        $("#MascotaId").val(id);
        $('#modal').modal('hide');
    });
    
    /*Listado de Mascota x Cliente*/
    function GetMascotasByCliente(ClienteId) {
        $.ajax({
            url: "/Cliente/GetMascotasByCliente",
            type: "GET",
            data: { id: ClienteId },
            success: function (r) {
                $(".mascotas").html("");
                if (Object.keys(r.Results).length > 0) {
                    $.each(r.Results, function (i, m) {
                        $(".mascotas").append('<tr class="mascota"><td>' + m.MascotaId + '</td><td>' + m.Nombre + '</td><td>' + m.FechaNacimiento + '</td></tr>');
                    });
                }
                else {
                    $(".mascotas").html("");
                    $(".mascotas").append('<tr><td colspan=3>El Cliente no tiene mascotas registradas</td></tr>');
                }
            },
            error: function (ex) {
                alert(ex.responseText);
            }
        })
    }
}(jQuery);

/*Ventana Modal*/
+function ($) {
    /*Abriendo Ventana Modal*/
    $("#btnCliente").click(function () {
        $('#modal').modal('show');
    });
    $("#btnOK").click(function () {
        $('#ModalMensaje').modal('hide');
    });
}(jQuery);

+function ($) {
    $("#frm-consulta").submit(function () {
        var form = $(this);
        mostrar2();
        form.ajaxSubmit({
            dataType: 'JSON',
            type: 'POST',
            url: '/Consulta/Crud',
            data: form.serialize(),
            success: function (r) {
                setTimeout(ocultar2, 1000);
                if (r.valor == 0) {
                    if (r.Message != null) {
                        $("#fechaValidacion").text(r.Message).show();
                    } else {
                        $("#fechaValidacion").text("").hide();
                    }
                } else {
                    $("#fechaValidacion").text("").hide();
                    $("#MostrandoMensaje").text(r.Message).show();
                    $('#ModalMensaje').modal({backdrop: false});
                    $("#ModalMensaje").modal('show');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                setTimeout(ocultar2, 1000);
                $("#MostrandoMensaje").text("Ha ocurrido un inconveniente").show();
                $('#ModalMensaje').modal({ backdrop: false });
                $("#ModalMensaje").modal('show');
            }
        });
        return false;
    });

}(jQuery);

/*Mostrar Preloader*/
function mostrar() {
    $(".modal").addClass("o-hidden");
    $(".container-preloader").show();
}
/*Ocultar Preloader*/
function ocultar() {
    $(".modal").removeClass("o-hidden");
    $(".container-preloader").hide();
}

/*Mostrar Preloader*/
function mostrar2() {
    $("body").addClass("o-hidden");
    $(".container-preloader").show();
}
/*Ocultar Preloader*/
function ocultar2() {
    $("body").removeClass("o-hidden");
    $(".container-preloader").hide();
}