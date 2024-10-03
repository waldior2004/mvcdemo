//var _root = "http://localhost:5963";
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
var _localeCal = {
    "format": "DD/MM/YYYY",
    "separator": " - ",
    "applyLabel": "Aplicar",
    "cancelLabel": "Cancelar",
    "fromLabel": "Desde",
    "toLabel": "Hasta",
    "customRangeLabel": "Configurar",
    "weekLabel": "Sem",
    "daysOfWeek": [
        "Do",
        "Lu",
        "Ma",
        "Mi",
        "Ju",
        "Vi",
        "Sa"
    ],
    "monthNames": [
        "Enero",
        "Febrero",
        "Marzo",
        "Abril",
        "Mayo",
        "Junio",
        "Julio",
        "Agosto",
        "Septiembre",
        "Octubre",
        "Noviembre",
        "Diciembre"
    ],
    "firstDay": 1
}
var _optionsCalSingle = {
    singleDatePicker: true,
    showDropdowns: true,
    singleClasses: "picker_4",
    showDropdowns: false,
    locale: _localeCal
};
var _optionsCalRange = {
    singleDatePicker: false,
    showDropdowns: true,
    startDate: moment(),
    locale: _localeCal,
    ranges: {
        'Proximos 7 Días': [moment(), moment().add(6, 'days')],
        'Proximos 30 Días': [moment(), moment().add(29, 'days')],
        'Proximos 60 Días': [moment(), moment().add(59, 'days')],
        'Proximos 90 Días': [moment(), moment().add(89, 'days')],
        'Mes Actual': [moment().startOf('month'), moment().endOf('month')],
        'Mes Pasado': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
    }
};
var _select = {
    style: 'os',
    info: false,
    items: 'row'
};
var _mimeDOC = "application/msword";
var _mimeDOCX = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
var _mimeXLS = "application/vnd.ms-excel";
var _mimeXLSX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
var _mimePDF = "application/pdf";
var _mimeEDIFACT = "application/EDIFACT"
var selected = [];
var selected_dc = [];
var selected_cp = [];
var selected_imp = [];

function createCookie(name, value, days) {
    var expires;

    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = encodeURIComponent(name) + "=" + encodeURIComponent(value) + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = encodeURIComponent(name) + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ')
            c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0)
            return decodeURIComponent(c.substring(nameEQ.length, c.length));
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

function showAutocomplete(target, url, param, IdReturn) {
    $(target).autocomplete({
        serviceUrl: url,
        dataType: "json",
        paramName: param,
        minChars: 4,
        zIndex: 2000,
        onInvalidateSelection: function () {
            $(IdReturn).val('0');
        },
        transformResult: function (response) {
            return {
                suggestions: $.map(response, function (dataItem) {
                    return { value: dataItem.value, data: dataItem.data };
                })
            };
        },
        onSelect: function (suggestion) {
            var icon = $(this).parent().find(".fa-check-circle").first();
            icon.css("color", "green");
            $(IdReturn).val(suggestion.data);
        }
    });
}

function showAutocompleteDesc(target, url, param, IdReturn, DescReturn) {
    $(target).autocomplete({
        serviceUrl: url,
        dataType: "json",
        paramName: param,
        minChars: 4,
        zIndex: 2000,
        onInvalidateSelection: function () {
            $(IdReturn).val('0');
        },
        transformResult: function (response) {
            return {
                suggestions: $.map(response, function (dataItem) {
                    return { value: dataItem.value, data: dataItem.data };
                })
            };
        },
        onSelect: function (suggestion) {
            var icon = $(this).parent().find(".fa-check-circle").first();
            icon.css("color", "green");
            $(IdReturn).val(suggestion.data);
            $(DescReturn).val(suggestion.value);
        }
    });
}

function showCascade(source, action, target, label) {
    $(source).on("change", function () {
        $.ajax({
            type: "POST",
            url: "/Cascade/" + action,
            data: { id: $(this).val() },
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response != undefined) {
                    $(target).empty();
                    $(target).append('<option value="0">[Seleccione ' + label + ']</option>');
                    $.each(response, function (key, value) {
                        $(target).append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
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
}

function showCascade2(source, action1, target1, label1, action2, target2, label2) {

    $(source).on("change", function () {
        $.ajax({
            type: "POST",
            url: "/Cascade/" + action1,
            data: { id: $(this).val() },
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response != undefined) {
                    $(target1).empty();
                    $(target1).append('<option value="0">[Seleccione ' + label1 + ']</option>');
                    $.each(response, function (key, value) {
                        $(target1).append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
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


    $(source).on("change", function () {
        $.ajax({
            type: "POST",
            url: "/Cascade/" + action2,
            data: { id: $(this).val() },
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response != undefined) {
                    $(target2).empty();
                    $(target2).append('<option value="0">[Seleccione ' + label2 + ']</option>');
                    $.each(response, function (key, value) {
                        $(target2).append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
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

}

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

function _selectBehaviourExtend(target, selections) {
    $(target + ' tbody').on('click', 'tr', function () {
        var id = this.id;
        var index = $.inArray(id, selections);
        if (index === -1) {
            selections.push(id);
        } else {
            selections.splice(index, 1);
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

    $("#myPassword .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: "/Account/ChangeP",
            data: { Usuario: $("#Usuario").val(), Clave: $("#CHPassword").val(), Reingrese: $("#CHReingrese").val() },
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response.Id == 0) {
                    $("#myPassword").modal("hide");
                    location.href = "/Account/Login";
                } else {
                    _setErrorMessage(response.Descripcion);
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

    $("#myProfile .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: "/Account/SetProfile",
            data: { id: $('#myProfile select:first').val() },
            dataType: "json",
            cache: false,
            success: function (response) {
                location.href = "/Home/Index";
            },
            failure: function (response) {
                _setErrorMessage("Error objeto ajax");
            },
            error: function (response) {
                _setErrorMessage("Error objeto ajax");
            }
        });
    });

    $(".submit").unbind().bind("click", function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "POST",
            url: "/Account/Login",
            data: $("#frmLogin").serialize(),
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response.Id != 0) {
                    if (response.Id == 9) {
                        _setErrorMessage(response.Message);
                    } else {
                        _setErrorMessage(response.Descripcion);
                    }
                } else {
                    var lst = $.parseJSON(response.Metodo);
                    createCookie("jwt", response.PilaError, 30);

                    if (response.Message == "Change") {
                        $("#myPassword").modal("show");
                    }
                    else
                    {
                        if (response.Metodo != undefined) {
                            $.each(lst, function (key, value) {
                                $('#myProfile select:first').append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
                            });
                            $("#myProfile").modal("show");
                        } else {
                            location.href = "/Home/Index";
                        }
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

    $("#myPassword .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: "/Account/ChangeP",
            data: { Usuario: $("a[name=CHPass]").attr("data-val-user"), Clave: $("#CHPassword").val(), Reingrese: $("#CHReingrese").val() },
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response.Id == 0) {
                    $("#myPassword").modal("hide");
                    location.href = "/Account/Login";
                } else {
                    _setErrorMessage(response.Descripcion);
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

    $("a[name=CHPass]").unbind().bind("click", function (evt) {
        evt.preventDefault();
        $("#CHPassword").val('');
        $("#CHReingrese").val('');
        $("#myPassword").modal("show");
    });

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
                    cache: false,
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
            cache: false,
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

    $("#myActionMultiple .btn-primary").unbind().bind("click", function (ev) {
        var urlAct = $(this).attr("data-val-url");
        var _params = { id: $("#myActionMultiple input:first").val() };

        var _added = $("#myActionMultiple .btn-primary").attr("data-val-aditional");
        if (_added !== "")
            $.extend(_params, { adicional: _added });

        $.ajax({
            type: "POST",
            url: _root + urlAct,
            data: _params,
            dataType: "json",
            cache: false,
            success: function (response) {
                $("#myActionMultiple").modal("hide");
                if (response.Id != 0) {
                    _setErrorMessage(response.Descripcion + (response.Message == null ? "" : "<br/>" + response.Message));
                } else {
                    if (urlAct == "/CargaLiquiC/Enviar")
                        _setSuccessMessage("Estimado Usuario, se procedió a enviar su registro de liquidación al área de logística." + (response.Message == null ? "" : "<br/>" + response.Message));
                    else
                        _setSuccessMessage(response.Descripcion + (response.Message == null ? "" : "<br/>" + response.Message));
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

function _formImprimir(target)
{
    $(target).unbind().bind("click", function (ev) {
        ev.preventDefault();
        window.open(_root + $(this).attr("data-val-url") + "/" + $(this).attr("data-val-id"), "_blank");
    });
}

function _formLinkInternal(target)
{
    $(target).unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { id: $(this).attr("data-val-id") },
            contentType: "json",
            cache: false,
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

function _formDetailDeleteGroup(name)
{
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

function _formLoad(name, _columns) {
    selected = [];

    var table = $("#datatable-responsive").DataTable({
        columns: _columns,
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formEditDelete(name);
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDelete(name);
    });

    _selectBehaviour();

    $(".new-item").unbind().bind("click", function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { id: 0 },
            contentType: "json",
            cache: false,
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

    _formDetailDeleteGroup(name);

    $("#my" + name + " .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: _root + $("#frm" + name).attr("action"),
            data: $("#frm" + name).serialize(),
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response.Id != 0) {
                    _setErrorMessage(response.Descripcion);
                } else {
                    $("#my" + name).modal("hide");
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

function _formLoadTarea(name, _columns) {
    selected = [];

    var table = $("#datatable-responsive").DataTable({
        columns: _columns,
        select: _select,
        language: _language,
        createdRow: function (row, data, dataIndex) {
            if (data[2] == "SI") {
                $(row).addClass("fila-azul");
            } else {
                $(row).addClass("fila-rojo");
            }
        },
        drawCallback: function (settings) {
            _formEditDelete(name);
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDelete(name);
    });

    _selectBehaviour();

    $(".new-item").unbind().bind("click", function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { id: 0 },
            contentType: "json",
            cache: false,
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

    _formDetailDeleteGroup(name);

    $("#my" + name + " .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: _root + $("#frm" + name).attr("action"),
            data: $("#frm" + name).serialize(),
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response.Id != 0) {
                    _setErrorMessage(response.Descripcion);
                } else {
                    $("#my" + name).modal("hide");
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
            cache: false,
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
            cache: false,
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

function _formGroupCustom(label, url, target)
{
    $(label).unbind().bind("click", function (ev) {
        ev.preventDefault();
        if (selected.length > 0) {
            var codes = [];
            $(selected).each(function (index) {
                codes.push(this.replace("row_", ""));
            });
            $("#myActionMultiple input:first").val(codes.join());
            $("#myActionMultiple .btn-primary").attr("data-val-url", url);
            $("#myActionMultiple .btn-primary").attr("data-val-target", target);
            $("#myActionMultiple").modal("show");
        }
        else
            _setErrorMessage("Debe de seleccionar al menos un registro para continuar");
    });
}

function _formActionCustom(label, url, target) {
    $(label).unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $("#myActionMultiple input:first").val($(this).attr("data-val-id"));
        $("#myActionMultiple .btn-primary").attr("data-val-url", url);
        $("#myActionMultiple .btn-primary").attr("data-val-target", target);
        $("#myActionMultiple").modal("show");
    });
}

function _formActionCustomAdditional(label, url, target) {
    $(label).unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        var id = $(this).attr("data-val-id");
        var stringAdd = $("#row_" + id).find("input:text").eq(0).val();
        if(stringAdd == "")
            stringAdd = $("#row_" + id).next().find("input:text").eq(0).val();
        $("#myActionMultiple input:first").val(id);
        $("#myActionMultiple .btn-primary").attr("data-val-url", url);
        $("#myActionMultiple .btn-primary").attr("data-val-target", target);
        $("#myActionMultiple .btn-primary").attr("data-val-aditional", stringAdd);
        $("#myActionMultiple").modal("show");
    });
}

function _formActionCustomDetail(target, idreference)
{
    $("#myActionDetail .btn-primary").unbind().bind("click", function (ev) {
        if ($("#Id").val() > 0) {
            $.ajax({
                type: "POST",
                url: _root + $(this).attr("data-val-url"),
                data: { id: $(idreference).val(), idPadre: $("#Id").val() },
                cache: false,
                success: function (response) {
                    $("#myActionDetail").modal("hide");

                    if (response.indexOf("Error de Sistema") >= 0)
                        _setErrorMessage(response);
                    else {
                        _setSuccessMessage("Registro(s) modificado(s) satisfactoriamente");
                        $(target).html(response);
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
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
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

    var table = $("#datatable-responsive").DataTable({
        columns: _columns,
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formEditDeleteExtend(name);
            if(name == "Externo")
                _formActionCustom(".reset-item", "/Externo/Reset", "/Externo/Index");
            else if (name == "Cotizacion") {
                _formActionCustom(".env-item", "/Cotizacion/Enviar", "/Cotizacion/Index");
                _formLinkInternal(".view-oc");
            }
            else if (name == "OrdenCompra") {
                _formActionCustom(".env-item", "/OrdenCompra/Enviar", "/OrdenCompra/Index");
                _formLinkInternal(".fact-item");
                _formImprimir(".imp-item");
            }
            else if (name == "Pedido") {
                _formLinkInternal(".view-coti");
                _formLinkInternal(".view-oc");
            }
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDeleteExtend(name);
        if (name == "Externo")
            _formActionCustom(".reset-item", "/Externo/Reset", "/Externo/Index");
        else if (name == "Cotizacion") {
            _formActionCustom(".env-item", "/Cotizacion/Enviar", "/Cotizacion/Index");
            _formLinkInternal(".view-oc");
        }
        else if (name == "OrdenCompra") {
            _formActionCustom(".env-item", "/OrdenCompra/Enviar", "/OrdenCompra/Index");
            _formLinkInternal(".fact-item");
            _formImprimir(".imp-item");
        }
        else if (name == "Pedido") {
            _formLinkInternal(".view-coti");
            _formLinkInternal(".view-oc");
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
            cache: false,
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

    _formDetailDeleteGroup(name);

}

function _formEditDeleteExtendDetail(target, name) {
    $(target + " .edit-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { id: $(this).attr("data-val-id") },
            contentType: "json",
            cache: false,
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
            cache: false,
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
            cache: false,
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
            cache: false,
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
            cache: false,
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
                cache: false,
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
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
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

/***********Mantenimiento de Perfiles**********/

function _formEditPerfil()
{
    selected = [];

    _formDetailExtend("Perfil");

    $("button[name=btnAgregar]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#ddlControl").val() > 0) {
                $.ajax({
                    type: "POST",
                    url: "/PerfilControl/Edit",
                    data: { Id: 0, IdPerfil: $("#Id").val(), IdControl: $("#ddlControl").val(), Estado: 1 },
                    cache: false,
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
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

    showCascade("#ddlPagina", "ControlesxPagina", "#ddlControl", "Control");

    _formDetailDeleteGroupExtend("PerfilControl");

    $(".ina-group, .act-group").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        var _estado = ($(this).attr("class") == "ina-group" ? 0 : 1);
        if ($("#Id").val() > 0) {
            if (selected.length > 0) {
                var codes = [];
                $(selected).each(function (index) {
                    codes.push(this.replace("row_", ""));
                });
                $.ajax({
                    type: "POST",
                    url: "/PerfilControl/EditGroup",
                    data: { Ids: codes.join(), IdPerfil: $("#Id").val(), Estado: _estado },
                    cache: false,
                    success: function (response) {
                        if (response.indexOf("Error de Sistema") >= 0)
                            _setErrorMessage(response);
                        else {
                            _setSuccessMessage("Registro(s) modificado(s) satisfactoriamente");
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
            }
            else
                _setErrorMessage("Debe de seleccionar al menos un registro para continuar");
        } else {
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

}

function _formEditPerfilControl() {
    selected = [];

    var table = $("#datatable-responsive").DataTable({
        columns: [null, null, null, null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formEditDeletePerfilControl();
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDeletePerfilControl();
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
                cache: false,
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
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
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
                    cache: false,
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
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

    _formDetailDeleteGroupExtend("UsuarioPerfil");

}

function _formEditUsuarioPerfil() {
    selected = [];

    var table = $("#datatable-responsive").DataTable({
        columns: [null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formEditDeleteUsuarioPerfil();
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDeleteUsuarioPerfil();
    });

    _selectBehaviour();

}

function _formEditDeleteUsuarioPerfil() {

    _formDetailDeleteExtend("UsuarioPerfil");

}
/**********************************************/

/***************Mantenimiento de Externos******************/
function _formLoadExternos() {
    _formGroupCustom(".reset-group", "/Externo/Reset", "/Externo/Index");
}

function _formEditExterno() {
    _formDetailExtend("Externo");
    $("button[name=btnAgregar]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#ddlPerfil").val() > 0) {
                $.ajax({
                    type: "POST",
                    url: "/ExternoPerfil/Edit",
                    data: { Id: 0, IdExterno: $("#Id").val(), IdPerfil: $("#ddlPerfil").val() },
                    cache: false,
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
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });
    _formDetailDeleteGroupExtend("ExternoPerfil");
}

function _formEditExternoPerfil() {
    selected = [];

    var table = $("#datatable-responsive").DataTable({
        columns: [null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formEditDeleteExternoPerfil();
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDeleteExternoPerfil();
    });

    _selectBehaviour();

}

function _formEditDeleteExternoPerfil() {
    _formDetailDeleteExtend("ExternoPerfil");
}
/*******************************************************/
