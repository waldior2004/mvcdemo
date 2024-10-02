var _root = "http://localhost:4006";
var _language = {
    "decimal": ".",
    "emptyTable": "La Tabla no tiene registros",
    "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
    "infoEmpty": "Mostrando 0 a 0 de 0 registros",
    "infoFiltered": "(filtrado de máximo _MAX_ registros)",
    "infoPostFix": "",
    "thousands": " ",
    "lengthMenu": "Mostrar _MENU_ registros",
    "loadingRecords": "Cargando...",
    "processing": "Procesando...",
    "search": "Buscar:",
    "zeroRecords": "No se encontraron registros",
    "paginate": {
        "first": "Primero",
        "last": "Último",
        "next": "Siguiente",
        "previous": "Anterior"
    },
    "aria": {
        "sortAscending": ": activar para ordenar de manera ascendente",
        "sortDescending": ": activar para ordenar de manera descendente"
    }
};
var _select = {
    style: 'os',
    info: false,
    items: 'row'
};
var selected = [];

function showPleaseWait() {
    $("#pleaseWaitDialog").modal("show");
}

function hidePleaseWait() {
    $("#pleaseWaitDialog").modal("hide");
}

function _setErrorMessage(descripcion) {
    $("#myError .modal-body > p > span").html(descripcion);
    $("#myError").modal("show");
}

function _setSuccessMessage(descripcion) {
    $("#mySuccess .modal-body > p > span").html(descripcion);
    $("#mySuccess").modal("show");
}

function _selectBehaviour()
{
    $('#datatable-responsive tbody').on('click', 'tr', function () {
        var id = this.id;
        var index = $.inArray(id, selected);
        if (index === -1) {
            selected.push(id);
        } else {
            selected.splice(index, 1);
        }
        $(this).toggleClass('selected');
    });
}

function _setToolTipsForm()
{
    $("div[data-toggle='tooltip']").tooltip();
}

/***********Inicio************/

function _formSubmitLogin() {
    $("#myProfile .btn-primary").click(function (ev) {
        $.ajax({
            type: "POST",
            url: "Account/SetProfile",
            data: { id: $('#myProfile select:first').val() },
            dataType: "json",
            success: function (response) {
                location.href = "Home/Index";
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });
    });

    $(".submit").click(function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "POST",
            url: "Account/Login",
            data: $("form").serialize(),
            dataType: "json",
            success: function (response) {
                if (response.Id != 0) {
                    if (response.Id == 9) {
                        _setErrorMessage(response.Message);
                    } else {
                        _setErrorMessage(response.Descripcion);
                    }
                } else {
                    var lst = $.parseJSON(response.Metodo);
                    if (response.Metodo != undefined) {
                        $.each(lst, function (key, value) {
                            $('#myProfile select:first').append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
                        });
                        $("#myProfile").modal("show");
                    } else {
                        location.href = "Home/Index";
                    }
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });
    });
}

function _formStart() {
    $("#sidebar-menu").find("a[href!='/Home/Index']").click(function (evt) {
        evt.preventDefault();
        var link = $(this).attr("href");
        if (link != undefined) {
            if (link.indexOf("/") >= 0) {
                $.ajax({
                    type: "GET",
                    url: _root + link,
                    data: null,
                    contentType: "json",
                    success: function (response) {
                        $("#content-inner-main").html(response);
                    },
                    failure: function (response) {
                        _setErrorMessage("Error objeto ajax");
                    },
                    error: function (response) {
                        _setErrorMessage("Error objeto ajax");
                    }
                });
            }
        }
    });

    $("#myDelete .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: _root + $(this).attr("data-val-url"),
            data: { id: $("#myDelete input:first").val() },
            dataType: "json",
            success: function (response) {
                $("#myDelete").modal("hide");
                if (response.Id != 0) {
                    _setErrorMessage(response.Descripcion + (response.Message == undefined ? "" : "<br/>" + response.Message));
                } else {
                    _setSuccessMessage(response.Descripcion + (response.Message == undefined ? "" : "<br/>" + response.Message));
                    $("#sidebar-menu").find("a[href='" + response.Metodo + "']").click();
                }

            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });
    });
}

/***********************/

/**Formulario Base**/

function _formLoad(name, _columns) {
    selected = [];

    $("#datatable-responsive").DataTable({
        columns: _columns,
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formEditDelete(name);
        }
    });

    _selectBehaviour();

    $(".new-item").unbind().bind("click", function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { id: 0 },
            contentType: "json",
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $("#my" + name + " .btn-primary").css("display", "inline");
                    $("#my" + name + " .modal-body").html(response);
                    $("#my" + name).modal("show");
                    _setToolTipsForm();
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });
    });

    $(".del-group").unbind().bind("click", function (ev) {
        ev.preventDefault();
        if (selected.length > 0) {
            var codes = [];
            $(selected).each(function (index) {
                codes.push(this.replace("row_", ""));
            });
            $("#myDelete input:first").val(codes.join());
            $("#myDelete .btn-primary").attr("data-val-url", "/" + name + "/Delete");
            $("#myDelete .btn-primary").attr("data-val-target", "/" + name + "/Index");
            $("#myDelete").modal("show");
        }
        else
            _setErrorMessage("Debe de seleccionar al menos un registro para continuar");
    });

    $("#my" + name + " .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: _root + $("#frm" + name).attr("action"),
            data: $("#frm" + name).serialize(),
            dataType: "json",
            success: function (response) {
                $("#my" + name).modal("hide");

                if (response.Id != 0) {
                    _setErrorMessage(response.Descripcion);
                } else {
                    _setSuccessMessage(response.Descripcion);
                    $("#sidebar-menu").find("a[href='" + response.Metodo + "']").click();
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });
    });

}

function _formEditDelete(name) {
    $(".view-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { id: $(this).attr("data-val-id") },
            contentType: "json",
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $("#my" + name + " .btn-primary").css("display", "none");
                    $("#my" + name + " .modal-body").html(response);
                    $("#my" + name).modal("show");
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });

    });

    $(".edit-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { id: $(this).attr("data-val-id") },
            contentType: "json",
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $("#my" + name + " .btn-primary").css("display", "inline");
                    $("#my" + name + " .modal-body").html(response);
                    $("#my" + name).modal("show");
                    _setToolTipsForm();
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });

    });

    $(".del-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $("#myDelete input:first").val($(this).attr("data-val-id"));
        $("#myDelete .btn-primary").attr("data-val-url", "/" + name + "/Delete");
        $("#myDelete .btn-primary").attr("data-val-target", "/" + name + "/Index");
        $("#myDelete").modal("show");
    });

}

/******************/

/*****Formulario Maestro-Detalle******/
function _formView() {
    $("#datatable-responsive").DataTable({
        language: _language
    });
}

function _formLoadExtend(name, _columns) {
    selected = [];

    $("#datatable-responsive").DataTable({
        columns: _columns,
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formEditDeleteExtend(name);
        }
    });

    _selectBehaviour();

    $(".new-item").unbind().bind("click", function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { id: 0 },
            contentType: "json",
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $("#content-inner-main").html(response);
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });
    });

    $(".del-group").unbind().bind("click", function (ev) {
        ev.preventDefault();
        if (selected.length > 0) {
            var codes = [];
            $(selected).each(function (index) {
                codes.push(this.replace("row_", ""));
            });
            $("#myDelete input:first").val(codes.join());
            $("#myDelete .btn-primary").attr("data-val-url", "/" + name + "/Delete");
            $("#myDelete .btn-primary").attr("data-val-target", "/" + name + "/Index");
            $("#myDelete").modal("show");
        }
        else
            _setErrorMessage("Debe de seleccionar al menos un registro para continuar");
    });
}

function _formEditDeleteExtend(name) {
    $(".view-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { id: $(this).attr("data-val-id") },
            contentType: "json",
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $("#content-inner-main").html(response);
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });

    });

    $(".edit-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { id: $(this).attr("data-val-id") },
            contentType: "json",
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $("#content-inner-main").html(response);
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });

    });

    $(".del-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $("#myDelete input:first").val($(this).attr("data-val-id"));
        $("#myDelete .btn-primary").attr("data-val-url", "/" + name + "/Delete");
        $("#myDelete .btn-primary").attr("data-val-target", "/" + name + "/Index");
        $("#myDelete").modal("show");
    });

}

function _formBack() {
    $(".back-item").unbind().bind("click", function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            contentType: "json",
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $("#content-inner-main").html(response);
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });
    });
}

function _formDetailExtend(name) {

    _setToolTipsForm();

    $("button[name=btnGrabar]").click(function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "POST",
            url: _root + $("#frm" + name).attr("action"),
            data: $("#frm" + name).serialize(),
            dataType: "json",
            success: function (response) {
                if (response.Id != 0) {
                    _setErrorMessage(response.Descripcion);
                } else {
                    if ($("#Id").val() == "0")
                        $("#Id").val(response.Metodo);
                    _setSuccessMessage(response.Descripcion);
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });
    });

    $("#myDeleteExtend .btn-primary").unbind().bind("click", function (ev) {
        if ($("#Id").val() > 0) {
            $.ajax({
                type: "POST",
                url: _root + $(this).attr("data-val-url"),
                data: { id: $("#myDeleteExtend input:first").val(), idPadre: $("#Id").val() },
                success: function (response) {
                    $("#myDeleteExtend").modal("hide");

                    if (response.indexOf("Error de Sistema") >= 0)
                        _setErrorMessage(response);
                    else {
                        _setSuccessMessage("Registro(s) eliminado(s) satisfactoriamente");
                        $(".x_content").html(response);
                    }
                },
                failure: function (response) {
                    _setErrorMessage("Error objeto ajax");
                },
                error: function (response) {
                    _setErrorMessage("Error objeto ajax");
                }
            });
        } else {
            _setErrorMessage("Antes de continuar debe guardar la descripción del perfil");
        }
    });

}

function _formDetailDeleteExtend(name) {
    $(".del-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $("#myDeleteExtend input:first").val($(this).attr("data-val-id"));
        $("#myDeleteExtend .btn-primary").attr("data-val-url", "/" + name + "/Delete");
        $("#myDeleteExtend").modal("show");
    });
}

function _formDetailDeleteGroupExtend(name) {
    $(".del-group").unbind().bind("click", function (ev) {
        ev.preventDefault();
        if (selected.length > 0) {
            var codes = [];
            $(selected).each(function (index) {
                codes.push(this.replace("row_", ""));
            });
            $("#myDeleteExtend input:first").val(codes.join());
            $("#myDeleteExtend .btn-primary").attr("data-val-url", "/" + name + "/Delete");
            $("#myDeleteExtend").modal("show");
        }
        else
            _setErrorMessage("Debe de seleccionar al menos un registro para continuar");
    });
}

/**************************************/

/***********Mantenimiento de Perfiles**********/

function _formEditPerfil()
{

    _formDetailExtend("Perfil");

    $("button[name=btnAgregar]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#ddlControl").val() > 0) {
                $.ajax({
                    type: "POST",
                    url: "/PerfilControl/Edit",
                    data: { Id: 0, IdPerfil: $("#Id").val(), IdControl: $("#ddlControl").val(), Estado: 1 },
                    success: function (response) {
                        if (response.indexOf("Error de Sistema") >= 0)
                            _setErrorMessage(response);
                        else {
                            _setSuccessMessage("Registro agregado satisfactoriamente");
                            $(".x_content").html(response);
                        }
                    },
                    failure: function (response) {
                        _setErrorMessage("Error objeto ajax");
                    },
                    error: function (response) {
                        _setErrorMessage("Error objeto ajax");
                    }
                });
            } else {
                _setErrorMessage("Debe de seleccionar un control");
            }
        } else {
            _setErrorMessage("Antes de continuar debe guardar la descripción del perfil");
        }
    });

    $("#ddlPagina").on("change", function () {
        $.ajax({
            type: "POST",
            url: "/Perfil/ControlesxPagina",
            data: { id: $(this).val() },
            dataType: "json",
            success: function (response) {
                if (response != undefined) {
                    $('#ddlControl').empty();
                    $('#ddlControl').append('<option value="0">[Seleccione Control]</option>');
                    $.each(response, function (key, value) {
                        $('#ddlControl').append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
                    });
                } else {
                    _setErrorMessage("Error al cargar controles");
                }
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });
    });

    _formDetailDeleteGroupExtend("PerfilControl");

}

function _formEditPerfilControl() {
    selected = [];

    $("#datatable-responsive").DataTable({
        columns: [null, null, null, null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formEditDeletePerfilControl();
        }
    });

    _selectBehaviour();

}

function _formEditDeletePerfilControl() {
    
    _formDetailDeleteExtend("PerfilControl");

    $(".ina-item, .act-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        var _estado = ($(this).attr("class") == "ina-item" ? 0 : 1);
        if ($("#Id").val() > 0) {
            $.ajax({
                type: "POST",
                url: "/PerfilControl/Edit",
                data: { Id: $(this).attr("data-val-id"), IdPerfil: $("#Id").val(), IdControl: 0, Estado: _estado },
                success: function (response) {
                    if (response.indexOf("Error de Sistema") >= 0)
                        _setErrorMessage(response);
                    else {
                        _setSuccessMessage("Registro modificado satisfactoriamente");
                        $(".x_content").html(response);
                    }
                },
                failure: function (response) {
                    _setErrorMessage("Error objeto ajax");
                },
                error: function (response) {
                    _setErrorMessage("Error objeto ajax");
                }
            });
        } else {
            _setErrorMessage("Antes de continuar debe guardar la descripción del perfil");
        }
    });
}

/**********************************************/

/***********Mantenimiento de Usuarios**********/
function _formEditUsuario() {

    _formDetailExtend("Usuario");

    $("button[name=btnAgregar]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#ddlPerfil").val() > 0) {
                $.ajax({
                    type: "POST",
                    url: "/UsuarioPerfil/Edit",
                    data: { Id: 0, IdUsuario: $("#Id").val(), IdPerfil: $("#ddlPerfil").val() },
                    success: function (response) {
                        if (response.indexOf("Error de Sistema") >= 0)
                            _setErrorMessage(response);
                        else {
                            _setSuccessMessage("Registro agregado satisfactoriamente");
                            $(".x_content").html(response);
                        }
                    },
                    failure: function (response) {
                        _setErrorMessage("Error objeto ajax");
                    },
                    error: function (response) {
                        _setErrorMessage("Error objeto ajax");
                    }
                });
            } else {
                _setErrorMessage("Debe de seleccionar un ");
            }
        } else {
            _setErrorMessage("Antes de continuar debe guardar la descripción del perfil");
        }
    });

    _formDetailDeleteGroupExtend("UsuarioPerfil");

}

function _formEditUsuarioPerfil() {
    selected = [];

    $("#datatable-responsive").DataTable({
        columns: [null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formEditDeleteUsuarioPerfil();
        }
    });

    _selectBehaviour();

}

function _formEditDeleteUsuarioPerfil() {

    _formDetailDeleteExtend("UsuarioPerfil");

}
/**********************************************/