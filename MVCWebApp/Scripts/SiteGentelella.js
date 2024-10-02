//var _root = "http://localhost";
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

//var selectedCli = [];
//var selectedTC = [];
/*DropZone*/
Dropzone.prototype.defaultOptions.dictDefaultMessage = "Arrastre sus archivos aquí";
Dropzone.prototype.defaultOptions.dictFallbackMessage = "Tu explorador no soporta alzar y arrastrar archivos.";
Dropzone.prototype.defaultOptions.dictFallbackText = "Por favor use el formulario inferior para subir sus archivos.";
Dropzone.prototype.defaultOptions.dictFileTooBig = "El archivo es demasiado grande ({{filesize}}MiB). Tamaño máximo de archivo: {{maxFilesize}}MiB.";
Dropzone.prototype.defaultOptions.dictInvalidFileType = "No se puede subir archivos de esta extensión.";
Dropzone.prototype.defaultOptions.dictResponseError = "El servidor respondió con código {{statusCode}}.";
Dropzone.prototype.defaultOptions.dictCancelUpload = "Cancelar Upload";
Dropzone.prototype.defaultOptions.dictCancelUploadConfirmation = "Esta seguro que desea cancelar el upload?";
Dropzone.prototype.defaultOptions.dictRemoveFile = "Borrar Archivo";
Dropzone.prototype.defaultOptions.dictMaxFilesExceeded = "No puede subir más archivos.";
/**/

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

function showAutocompleteProducto(target, url, param, IdReturn, IdDescripcion, IdUnidadMedida, IdPrecio) {
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
                    return { value: dataItem.value, data: dataItem.data, unimed: dataItem.unimed, precio: dataItem.precio };
                })
            };
        },
        onSelect: function (suggestion) {
            var icon = $(this).parent().find(".fa-check-circle").first();
            icon.css("color", "green");
            $(IdReturn).val(suggestion.data);
            $(IdDescripcion).val(suggestion.value);
            $(IdUnidadMedida).text(suggestion.unimed);
            $(IdPrecio).val(suggestion.precio);
        }
    });
}

function showAutocompleteProductoxProv(target, url, param, IdReturn, IdDescripcion, IdUnidadMedida, IdPrecio, IdTarifa, IdCantidad, IdTotal) {
    $(target).autocomplete({
        serviceUrl: url,
        dataType: "json",
        paramName: param,
        params:  {
            id: function () {
                return $("#IdProveedor").val();
            }
        },
        minChars: 4,
        zIndex: 2000,
        onInvalidateSelection: function () {
            $(IdReturn).val('0');
        },
        transformResult: function (response) {
            return {
                suggestions: $.map(response, function (dataItem) {
                    return { value: dataItem.value, data: dataItem.data, unimed: dataItem.unimed, precio: dataItem.precio, idtar: dataItem.idtar };
                })
            };
        },
        onSelect: function (suggestion) {
            var icon = $(this).parent().find(".fa-check-circle").first();
            icon.css("color", "green");
            $(IdReturn).val(suggestion.data);
            $(IdDescripcion).val(suggestion.value);
            $(IdUnidadMedida).text(suggestion.unimed);
            $(IdPrecio).val(suggestion.precio);
            $(IdTarifa).val(suggestion.idtar);

            if (parseInt($(IdCantidad).val()) == NaN) {
                $(IdCantidad).val("0");
                $(IdTotal).val("0.000");
            } else {
                $(IdTotal).val(parseFloat(suggestion.precio * parseInt($(IdCantidad).val())).toFixed(3));
            }

        }
    });
}

function showAutocompleteProveedor(target, url, param, params, IdReturn, IdDescripcion, IdTarifa, IdPrecio, IdCantidad, IdTotal) {
    $(target).autocomplete({
        serviceUrl: url,
        dataType: "json",
        paramName: param,
        params: params,
        minChars: 4,
        zIndex: 2000,
        onInvalidateSelection: function () {
            $(IdReturn).val('0');
        },
        transformResult: function (response) {
            return {
                suggestions: $.map(response, function (dataItem) {
                    return { value: dataItem.value, data: dataItem.data, unimed: dataItem.unimed, precio: dataItem.precio };
                })
            };
        },
        onSelect: function (suggestion) {
            $(IdReturn).val(suggestion.data);
            $(IdDescripcion).val(suggestion.value);
            $(IdTarifa).val(suggestion.unimed);
            $(IdPrecio).val(suggestion.precio);
            var cant = $(IdCantidad).text();
            $(IdTotal).text((parseFloat(suggestion.precio) * cant).toFixed(3));

            if (suggestion.unimed == "0") {
                $(IdTarifa).parent().parent().css("color", "Red");
                $(IdTarifa).parent().parent().css("font-weight", "bold");
            } else {
                $(IdTarifa).parent().parent().css("color", "Black");
                $(IdTarifa).parent().parent().css("font-weight", "normal");
            }
        }
    });
}

function showAutocompleteViaje(target, url, param, IdReturn, ddlTarget, actioncascade, label, ddlPuerto) {
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

            $.ajax({
                type: "POST",
                url: "/Cascade/" + actioncascade,
                data: { id: suggestion.data, port: $(ddlPuerto).val() },
                dataType: "json",
                cache: false,
                success: function (response) {
                    if (response != undefined) {
                        $(ddlTarget).empty();
                        $(ddlTarget).append('<option value="0">[Seleccione ' + label + ']</option>');
                        $.each(response, function (key, value) {
                             $(ddlTarget).append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
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
        }
    });
}

function showCascadeViaje() {
    $("#Puerto").on("change", function () {
        $.ajax({
            type: "POST",
            url: "/Cascade/ViajesxNave",
            data: { id: $("#IdNave").val(), port: $(this).val() },
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response != undefined) {
                    $("#ddlViaje").empty();
                    $("#ddlViaje").append('<option value="0">[Seleccione Viaje]</option>');
                    $.each(response, function (key, value) {
                        $("#ddlViaje").append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
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

function showCascadeUbigeo(Pais, Departamento, Provincia, Distrito)
{
    $(Pais).on("change", function () {
        $.ajax({
            type: "POST",
            url: "/Cascade/DepartamentoxPais",
            data: { id: $(this).val() },
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response != undefined) {
                    $(Departamento).empty();
                    $(Departamento).append('<option value="0">[Seleccione Departamento]</option>');

                    $(Provincia).empty();
                    $(Provincia).append('<option value="0">[Seleccione Provincia]</option>');

                    $(Distrito).empty();
                    $(Distrito).append('<option value="0">[Seleccione Distrito]</option>');

                    $.each(response, function (key, value) {
                        $(Departamento).append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
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

    $(Departamento).on("change", function () {
        $.ajax({
            type: "POST",
            url: "/Cascade/ProvinciaxDepartamento",
            data: { id: $(this).val() },
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response != undefined) {
                    $(Provincia).empty();
                    $(Provincia).append('<option value="0">[Seleccione Provincia]</option>');

                    $(Distrito).empty();
                    $(Distrito).append('<option value="0">[Seleccione Distrito]</option>');

                    $.each(response, function (key, value) {
                        $(Provincia).append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
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

    $(Provincia).on("change", function () {
        $.ajax({
            type: "POST",
            url: "/Cascade/DistritoxProvincia",
            data: { id: $(this).val() },
            dataType: "json",
            cache: false,
            success: function (response) {
                if (response != undefined) {
                    $(Distrito).empty();
                    $(Distrito).append('<option value="0">[Seleccione Distrito]</option>');

                    $.each(response, function (key, value) {
                        $(Distrito).append('<option value=' + value.Id + '>' + value.Descripcion + '</option>');
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

function _setUploadControl(container, target, ruta, rutaDelete, rutaView, maxFSize, maxFiles, accept, rutaDownload) {
    Dropzone.autoDiscover = false;

    var previewNode = document.querySelector(container + " #template");
    previewNode.id = "";
    var previewTemplate = previewNode.parentNode.innerHTML;
    previewNode.parentNode.removeChild(previewNode);

    $(target).dropzone({ 
            url: ruta,
            paramName: "file",
            maxFilesize: maxFSize,
            maxFiles: maxFiles,
            thumbnailWidth: 80,
            thumbnailHeight: 80,
            parallelUploads: 20,
            previewTemplate: previewTemplate,
            autoQueue: false, 
            previewsContainer: container + " #previews",
            acceptedFiles: accept.join(),
            clickable: container + " .fileinput-button",
            dictRemoveFileConfirmation: "Seguro que desea eliminar el documento?",
            init: function () {
                var imgDropzone = Dropzone.forElement(target);

                if ($("#Id").val() != "0") {

                    $.ajax({
                        type: "POST",
                        url: rutaView,
                        data: { id: $("#Id").val() },
                        cache: false,
                        success: function (response) {
                            $.each(response, function (index, value) {
                                var mockFile = { name: value.name, size: value.size, type: value.type, serverId: value.Id };
                                imgDropzone.options.addedfile.call(imgDropzone, mockFile);
                                imgDropzone.options.thumbnail.call(imgDropzone, mockFile, "/Content/images/upload.png");
                                mockFile.previewElement.classList.add('dz-success');
                                mockFile.previewElement.classList.add('dz-complete');

                                var a = document.createElement('button');
                                a.setAttribute('onclick', "window.open('" + _root + rutaDownload + value.Id + "', '_blank')");
                                a.classList.add("btn");
                                a.classList.add("btn-warning");
                                a.innerHTML = "<i class='glyphicon glyphicon-download'></i><span>Descargar</span>";
                                $(a).insertAfter(mockFile.previewElement.querySelector(".delete"));
                            });
                        },
                        failure: function (response) {
                            _setErrorMessage("Error objeto ajax");
                        },
                        error: function (response) {
                            _setErrorMessage("Error objeto ajax");
                        }
                    });
                }

                this.on("maxfilesexceeded", function (file) {
                    alert("Ha excedido el límite de archivos a subir!");
                });

                this.on("addedfile", function (file) {
                    if ($("#Id").val() == "0") {
                        _setErrorMessage("Debe de grabar los datos de la cabecera para continuar");
                        this.removeFile(file);
                    }
                    else {
                        file.previewElement.querySelector(container + " .start").onclick = function () {
                            imgDropzone.enqueueFile(file);
                        };
                    }
                });
                this.on("totaluploadprogress", function (progress) {
                    document.querySelector(container + " #total-progress .progress-bar").style.width = progress + "%";
                });
                this.on("sending", function (file, xhr, formData) {
                    formData.append("id", $("#Id").val());
                    document.querySelector(container + " #total-progress").style.opacity = "1";
                    file.previewElement.querySelector(container + " .start").setAttribute("disabled", "disabled");
                });
                this.on("queuecomplete", function (progress) {
                    document.querySelector(container + " #total-progress").style.opacity = "0";
                });
                this.on("success", function (file, resp) {
                    file.serverId = resp.Id;
                    var a = document.createElement('button');
                    a.setAttribute('onclick', "window.open('" + _root + rutaDownload + resp.Id + "', '_blank')");
                    a.classList.add("btn");
                    a.classList.add("btn-warning");
                    a.innerHTML = "<i class='glyphicon glyphicon-download'></i><span>Descargar</span>";
                    $(a).insertAfter(file.previewTemplate.querySelector(container + " .delete"));
                });
                this.on("removedfile", function (file) {
                    if ($("#Id").val() != "0" && file.serverId != undefined) {
                        $.ajax({
                            type: "POST",
                            url: _root + rutaDelete,
                            data: { id: file.serverId },
                            dataType: "json",
                            cache: false,
                            success: function (response) {
                                if (response.Id != 0) {
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
                    }
                });
                document.querySelector(container + " #actions .start").onclick = function () {
                    imgDropzone.enqueueFiles(imgDropzone.getFilesWithStatus(Dropzone.ADDED));
                };
                document.querySelector(container + " #actions .cancel").onclick = function () {
                    imgDropzone.removeAllFiles(true);
                };
            },
        });
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

function _formHistory()
{
    $(".hist-group").click(function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: null,
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

function _formDetailViewExtend(name) {
    $(".detail-item").unbind().bind("click", function (ev) {
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

function _formDetailDeleteTarget(target, name) {
    $(target + " .del-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $("#myDeleteExtend input:first").val($(this).attr("data-val-id"));
        $("#myDeleteExtend .btn-primary").attr("data-val-url", "/" + name + "/Delete");
        $("#myDeleteExtend").modal("show");
    });
}

function _formDetailDeleteTarget2(target, name) {
    $(target + " .del-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $("#myDeleteExtend2 input:first").val($(this).attr("data-val-id"));
        $("#myDeleteExtend2 .btn-primary").attr("data-val-url", "/" + name + "/Delete");
        $("#myDeleteExtend2").modal("show");
    });
}

function _formDetailDeleteTarget3(target, name) {
    $(target + " .del-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $("#myDeleteExtend3 input:first").val($(this).attr("data-val-id"));
        $("#myDeleteExtend3 .btn-primary").attr("data-val-url", "/" + name + "/Delete");
        $("#myDeleteExtend3").modal("show");
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

/****************Mantenimiento de Tarifas de Condiciones de Energia***************/
function _formLoadTarifaCEOs() {
    _formGroupCustom(".apro-group", "/TarifaCE/Aprobar", "/TarifaCE/Index");
    _formActionCustom(".apro-item", "/TarifaCE/Aprobar", "/TarifaCE/Index");
    _formHistory();
}

function _formEditTarifaCE() {

    _formDetailExtend("TarifaCE");

    _formDetailDeleteGroupExtend("TarifaCEDoc");

}

function _formEditTarifaCEDoc() {
    selected = [];

    var table = $("#datatable-responsive").DataTable({
        columns: [null, null, null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formEditDeleteTarifaCEDoc();
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDeleteTarifaCEDoc();
    });

    _selectBehaviour();

}

function _formEditDeleteTarifaCEDoc() {

    _formDetailDeleteExtend("TarifaCEDoc");

}
/******************************************************************/

/*******************Mantenimiento de Condiciones Especiales*****************************/

function _formLoadTabCondEspeDetail(tableId, TabId, TipoId)
{
    $(tableId + " .view-item").unbind().bind("click", function (ev) {
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

    $(tableId + " .edit-item").unbind().bind("click", function (ev) {
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

    $(tableId + " .del-item").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        $("#myDeleteTab input:first").val($(this).attr("data-val-id"));
        $("#myDeleteTab .btn-primary").attr("data-val-url", "/CondEspeCli/Delete");
        $("#myDeleteTab .btn-primary").attr("data-val-target", TipoId);
        $("#myDeleteTab").modal("show");
    });
}

function _formLoadTabCondEspe(tableId, TabId, TipoId)
{
    selected = [];

    var table = $(tableId).DataTable({
        columns: [null, null, null, null, { orderable: false }],
        select: selected,
        language: _language,
        drawCallback: function (settings) {
            _formLoadTabCondEspeDetail(tableId, TabId, TipoId);
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formLoadTabCondEspeDetail(tableId, TabId, TipoId);
    });

    $(tableId + ' tbody').on('click', 'tr', function () {
        var id = this.id;
        var index = $.inArray(id, selected);
        if (index === -1) {
            selected.push(id);
        } else {
            selected.splice(index, 1);
        }
        $(this).toggleClass('selected');
    });

    $(TabId + " .new-item").unbind().bind("click", function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { Tipo: TipoId },
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

    $(TabId + " .del-group").unbind().bind("click", function (ev) {
        ev.preventDefault();
        if (selected.length > 0) {
            var codes = [];
            $(selected).each(function (index) {
                codes.push(this.replace("row_", ""));
            });
            $("#myDeleteTab input:first").val(codes.join());
            $("#myDeleteTab .btn-primary").attr("data-val-url", "/CondEspeCli/Delete");
            $("#myDeleteTab .btn-primary").attr("data-val-target", TipoId);
            $("#myDeleteTab").modal("show");
        }
        else
            _setErrorMessage("Debe de seleccionar al menos un registro para continuar");
    });

    $(TabId + " .hist-group").click(function (ev) {
        ev.preventDefault();
        var _tipo = $(this).attr("data-val-tipo");
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { Tipo: _tipo },
            cache: false,
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    if (_tipo == "Cliente")
                        $("#tab_content1").html(response);
                    else if (_tipo == "TipoCarga")
                        $("#tab_content2").html(response);
                    else
                        $("#tab_content3").html(response);
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

    $("#myDeleteTab .btn-primary").unbind().bind("click", function (ev) {
        var _target = $(this).attr("data-val-target");
        $.ajax({
            type: "POST",
            url: _root + $(this).attr("data-val-url"),
            data: { id: $("#myDeleteTab input:first").val(), Tipo: _target },
            cache: false,
            success: function (response) {
                $("#myDeleteTab").modal("hide");
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    if(_target == "Cliente")
                        $("#tab_content1").html(response);
                    else if (_target == "TipoCarga")
                        $("#tab_content2").html(response);
                    else
                        $("#tab_content3").html(response);
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

function _formEditTabCondEspe()
{

    _formBack();

    _setUploadControl(".x_content2", ".dropzone", "/Upload/CondEspeCli", "/CondEspeCliDoc/Delete", "/CondEspeCliDoc/ViewDocs", 5, 5, [_mimeDOC, _mimeDOCX, _mimeXLS, _mimeXLSX, _mimePDF], "/Upload/DownloadCondEspeCli/");

    _formDetailExtend("CondEspeCli");

    $("button[name=btnAgregar]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#ddlTerminal").val() > 0 && $("#ddlRetiraPor").val() > 0 && $("#NumDias").val() > 0 && $("#NumDias").val() != undefined) {
                $.ajax({
                    type: "POST",
                    url: "/CondEspeCliDetalle/Edit",
                    data: { Id: 0, IdCondEspeCli: $("#Id").val(), IdTerminal: $("#ddlTerminal").val(), IdRetiraPor: $("#ddlRetiraPor").val(), Dias: $("#NumDias").val() },
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
                _setErrorMessage("Falta Rellenar un Campo");
            }
        } else {
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

    _formDetailDeleteGroupExtend("CondEspeCliDetalle");

    $("#myCondEspeCliDetalle .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: _root + $("#frmCondEspeCliDetalle").attr("action"),
            data: $("#frmCondEspeCliDetalle").serialize(),
            cache: false,
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $(".x_content").html(response);
                    $("#myCondEspeCliDetalle").modal("hide");
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

function _formEditTabDetalle()
{
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
                    $("#myCondEspeCliDetalle .modal-body").html(response);
                    $("#myCondEspeCliDetalle").modal("show");
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

    $(".dia-item").unbind().bind("click", function (ev) {
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
                    $("#myCondEspeCliDia .modal-body").html(response);
                    $("#myCondEspeCliDia").modal("show");
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

function _formEditTabDias()
{
    $("#myCondEspeCliDia .btn-primary").unbind().bind("click", function (ev) {
        ev.preventDefault();
        $.ajax({
            type: "POST",
            url: _root + $("#frmCondEspeCliDia").attr("action"),
            data: $("#frmCondEspeCliDia").serialize(),
            cache: false,
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $("#table-dias").html(response);
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

function _formEditTabDiasTable()
{
    $("#datatable-dias .del-item").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: _root + "/CondEspeCliDia/Delete",
            data: { id: $(this).attr("data-val-id"), idPadre: $("#IdCondEspeCliDetalle").val() },
            cache: false,
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $("#table-dias").html(response);
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

function _formAsigDias()
{
    $(".asig-item").unbind().bind("click", function (ev) {
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
                    $("#myCondEspeCliDia .modal-body").html(response);
                    $("#myCondEspeCliDia").modal("show");
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

function _formViewTabDetail()
{
    var table = $("#datatable-detalle").DataTable({
        language: _language,
        drawCallback: function (settings) {
            _formAsigDias();
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formAsigDias();
    });
}

function _formEditTabHistorial()
{
    $(".back-histo").click(function (ev) {
        ev.preventDefault();
        var _tipo = $(this).attr("data-val-tipo");
        $.ajax({
            type: "GET",
            url: _root + $(this).attr("data-val-url"),
            data: { Tipo: _tipo },
            cache: false,
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    if (_tipo == "BackCliente")
                        $("#tab_content1").html(response);
                    else if (_tipo == "BackTipoCarga")
                        $("#tab_content2").html(response);
                    else
                        $("#tab_content3").html(response);
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

function _formEditTabHistorialDetail()
{
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
}
/******************************************************************/

/*******************Proceso de Carga Archivo Liquidación****************/

function _formCargaLiquidacion() {
    selected = [];
    Dropzone.autoDiscover = false;

    var table = $("#datatable-responsive").DataTable({
        select: _select,
        language: _language,
        sorting: false,
        drawCallback: function (settings) {
            _formEditDeleteExtend("CargaLiquiC");
            _formActionCustom(".send-item", "/CargaLiquiC/Enviar", "/CargaLiquiC/Index");
            $(".down-item").unbind().bind("click", function (ev) {
                ev.stopPropagation();
                ev.preventDefault();
                var rutaDown = _root + "/Upload/DownloadCargaLiquiC/" + $(this).attr("data-val-id");
                window.open(rutaDown, '_blank');
            });
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDeleteExtend("CargaLiquiC");
        _formActionCustom(".send-item", "/CargaLiquiC/Enviar", "/CargaLiquiC/Index");
        $(".down-item").unbind().bind("click", function (ev) {
            ev.stopPropagation();
            ev.preventDefault();
            var rutaDown = _root + "/Upload/DownloadCargaLiquiC/" + $(this).attr("data-val-id");
            window.open(rutaDown, '_blank');
        });
    });

    _selectBehaviour();

    _formDetailDeleteGroup("CargaLiquiC");

    $("#actions .update").unbind().bind("click", function (evt) {
        $.ajax({
            type: "GET",
            url: _root + "/CargaliquiC/Index",
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
    });

    $("#actions .down").unbind().bind("click", function (evt) {
        window.open(_root + '/Formatos/FormatoExcelMSC.xls', '_blank');
    });

    $("input[type=radio]").unbind().bind("click", function (evt) {
        if ($(this).val() == 1)
            $(this).parent().parent().prev().find("label").eq(0).text("Seleccione el archivo Excel de liquidación");
        else
            $(this).parent().parent().prev().find("label").eq(0).text("Seleccione el archivo EDI de entrada y luego el archivo EDI de salida");
    });

    $(".dropzone").dropzone({
        url: "/Upload/CargaLiquiC",
        maxFilesize: 5,
        maxFiles: 1,
        autoQueue: false,
        clickable: ".fileinput-button",
        init: function () {
            var imgDropzone = Dropzone.forElement(".dropzone");

            this.on("maxfilesexceeded", function (file) {
                alert("Ha excedido el límite de archivos a subir!");
                this.removeFile(file);
            });

            this.on("addedfile", function (file) {
                if ($("#Terminal").val() == "0" || $("#IdNave").val() == "0" || $("#ddlViaje").val() == "0" || $("#Puerto").val() == "0") {
                    _setErrorMessage("Debe de seleccionar la terminal, la nave, el puerto y el viaje, para continuar");
                    this.removeFile(file);
                }
                else {
                    document.querySelectorAll(".msgUpload")[0].innerText = "(OK)";
                }
            });

            this.on("totaluploadprogress", function (progress) {
                document.querySelector("#total-progress .progress-bar").style.width = progress + "%";
            });

            this.on("sending", function (file, xhr, formData) {
                formData.append("id", $("#Terminal").val());
                formData.append("idNave", $("#IdNave").val());
                formData.append("idViaje", $("#ddlViaje").val());
                formData.append("idTipo", $("#ddlTipo").val());
                formData.append("idPort", $("#Puerto").val());
                document.querySelector("#total-progress").style.opacity = "1";
                document.querySelector("#actions .start").setAttribute("disabled", "disabled");
            });

            this.on("queuecomplete", function (progress) {
                document.querySelector("#total-progress").style.opacity = "0";
            });

            this.on("success", function (file, resp) {
                document.querySelector("#actions .start").removeAttribute("disabled");
                document.querySelector("#actions .update").click();
            });

            document.querySelector("#actions .start").onclick = function () {
                imgDropzone.enqueueFiles(imgDropzone.getFilesWithStatus(Dropzone.ADDED));
            };
        },
    });
}

function _formCargaLiquidaciondetail() {
    $(".liqui-item").unbind().bind("click", function (ev) {
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
                    $("#myCargaLiqui .modal-body").html(response);
                    $("#myCargaLiqui").modal("show");
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

/*******************Mantenimiento tarifario documentos****************/
function _formEditTarifario() {

    _formDetailExtend("Tarifario");

    _formDetailDeleteGroupExtend("TarifarioDoc");

}

function _formEditTarifarioDoc() {
    selected = [];

    $("#datatable-responsive").DataTable({
        columns: [null, null, null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formDetailDeleteExtend("TarifarioDoc");
        }
    });

    _selectBehaviour();

}
/******************************************************************************/

/************************ Valida Liquidación **********************************/

function _formValidaLiquidacion() {
    selected = [];
    
    var table = $("#datatable-responsive").DataTable({
        select: _select,
        language: _language,
        sorting: false,
        drawCallback: function (settings) {
            _formEditDeleteExtend("CargaLiquiC");
            _formActionCustomAdditional(".apro-item", "/ValidarLiquiC/Aprobar", "/ValidarLiquiC/Index");
            _formActionCustomAdditional(".rech-item", "/ValidarLiquiC/Rechazar", "/ValidarLiquiC/Index");
            $(".down-item").unbind().bind("click", function (ev) {
                ev.stopPropagation();
                ev.preventDefault();
                var rutaDown = _root + "/Upload/DownloadCargaLiquiC/" + $(this).attr("data-val-id");
                window.open(rutaDown, '_blank');
            });
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDeleteExtend("CargaLiquiC");
        _formActionCustomAdditional(".apro-item", "/ValidarLiquiC/Aprobar", "/ValidarLiquiC/Index");
        _formActionCustomAdditional(".rech-item", "/ValidarLiquiC/Rechazar", "/ValidarLiquiC/Index");
        $(".down-item").unbind().bind("click", function (ev) {
            ev.stopPropagation();
            ev.preventDefault();
            var rutaDown = _root + "/Upload/DownloadCargaLiquiC/" + $(this).attr("data-val-id");
            window.open(rutaDown, '_blank');
        });
    });

    _selectBehaviour();

}

/*******************************************************************/

/***********************************Proveedor******************************************/

function _formDetailDeleteGroupProveedor(target, name) {
    $(target + " .del-group").unbind().bind("click", function (ev) {
        ev.preventDefault();
        if (name == "DatoComercial")
            selections = selected_dc;
        else if(name == "ContactoProveedor")
            selections = selected_cp;
        else
            selections = selected_imp;

        if (selections.length > 0) {
            var codes = [];
            $(selections).each(function (index) {
                codes.push(this.replace("row_", ""));
            });
            if (name == "DatoComercial") {
                $("#myDeleteExtend input:first").val(codes.join());
                $("#myDeleteExtend .btn-primary").attr("data-val-url", "/" + name + "/Delete");
                $("#myDeleteExtend").modal("show");
            }
            else if (name == "ContactoProveedor") {
                $("#myDeleteExtend2 input:first").val(codes.join());
                $("#myDeleteExtend2 .btn-primary").attr("data-val-url", "/" + name + "/Delete");
                $("#myDeleteExtend2").modal("show");
            }
            else {
                $("#myDeleteExtend3 input:first").val(codes.join());
                $("#myDeleteExtend3 .btn-primary").attr("data-val-url", "/" + name + "/Delete");
                $("#myDeleteExtend3").modal("show");
            }
        }
        else
            _setErrorMessage("Debe de seleccionar al menos un registro para continuar");
    });
}

function _formEditProveedor() {

    _formBack();

    _formDetailExtend("Proveedor");

    $("#myDeleteExtend2 .btn-primary").unbind().bind("click", function (ev) {
        if ($("#Id").val() > 0) {
            $.ajax({
                type: "POST",
                url: _root + $(this).attr("data-val-url"),
                data: { id: $("#myDeleteExtend2 input:first").val(), idPadre: $("#Id").val() },
                cache: false,
                success: function (response) {
                    $("#myDeleteExtend2").modal("hide");

                    if (response.indexOf("Error de Sistema") >= 0)
                        _setErrorMessage(response);
                    else {
                        _setSuccessMessage("Registro(s) eliminado(s) satisfactoriamente");
                        $(".x_content2").html(response);
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

    $("#myDeleteExtend3 .btn-primary").unbind().bind("click", function (ev) {
        if ($("#Id").val() > 0) {
            $.ajax({
                type: "POST",
                url: _root + $(this).attr("data-val-url"),
                data: { id: $("#myDeleteExtend3 input:first").val(), idPadre: $("#Id").val() },
                cache: false,
                success: function (response) {
                    $("#myDeleteExtend3").modal("hide");

                    if (response.indexOf("Error de Sistema") >= 0)
                        _setErrorMessage(response);
                    else {
                        _setSuccessMessage("Registro(s) eliminado(s) satisfactoriamente");
                        $(".x_content3").html(response);
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

    $("button[name=btnAgregar]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#ddlPais").val() > 0 && $("#ddlBanco").val() > 0 && $("#ddlTipoCuenta").val() > 0 && $("#NumCuenta").val() != "" && $("#ddlTipoInterlocutor").val() != "" && $("#NumCCI").val() != "" && $("#NumSwift").val() != "") {
                $.ajax({
                    type: "POST",
                    url: "/DatoComercial/Edit",
                    data: {
                        Id: 0,
                        IdProveedor: $("#Id").val(),
                        IdPais2: $("#ddlPais").val(),
                        IdBanco: $("#ddlBanco").val(),
                        IdTipoCuenta: $("#ddlTipoCuenta").val(),
                        NroCuenta: $("#NumCuenta").val(),
                        IdTipoInterlocutor: $("#ddlTipoInterlocutor").val(),
                        NroCCI: $("#NumCCI").val(),
                        Swift: $("#NumSwift").val()
                    },
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
                _setErrorMessage("Falta Rellenar un Campo");
            }
        } else {
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

    $("button[name=btnAgregarCont]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#ddlCargo").val() > 0 && $("#NombreCompleto").val() != "" && $("#CorreoCont").val() != "") {
                $.ajax({
                    type: "POST",
                    url: "/ContactoProveedor/Edit",
                    data: { Id: 0, IdProveedor: $("#Id").val(), IdCargo: $("#ddlCargo").val(), NombreCompleto: $("#NombreCompleto").val(), Correo: $("#CorreoCont").val(), IndContacto: ($("#IndicadorCont").is(":checked") ? 1 : 0) },
                    cache: false,
                    success: function (response) {
                        if (response.indexOf("Error de Sistema") >= 0)
                            _setErrorMessage(response);
                        else {
                            _setSuccessMessage("Registro agregado satisfactoriamente");
                            $(".x_content2").html(response);
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
                _setErrorMessage("Falta Rellenar un Campo");
            }
        } else {
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

    $("button[name=btnAgregarImp]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#ddlImpuesto").val() > 0) {
                $.ajax({
                    type: "POST",
                    url: "/ImpuestoProveedor/Edit",
                    data: { Id: 0, IdProveedor: $("#Id").val(), IdImpuesto: $("#ddlImpuesto").val() },
                    cache: false,
                    success: function (response) {
                        if (response.indexOf("Error de Sistema") >= 0)
                            _setErrorMessage(response);
                        else {
                            _setSuccessMessage("Registro agregado satisfactoriamente");
                            $(".x_content3").html(response);
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
                _setErrorMessage("Falta seleccionar el impuesto");
            }
        } else {
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

    _formDetailDeleteGroupProveedor(".x_title:eq(0)", "DatoComercial");

    _formDetailDeleteGroupProveedor(".x_title:eq(1)", "ContactoProveedor");

    _formDetailDeleteGroupProveedor(".x_title:eq(2)", "ImpuestoProveedor");

    $("#myDatoComercial .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: _root + $("#frmDatoComercial").attr("action"),
            data: $("#frmDatoComercial").serialize(),
            cache: false,
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $(".x_content").html(response);
                    $("#myDatoComercial").modal("hide");
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

    $("#myContactoProveedor .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: _root + $("#frmContactoProveedor").attr("action"),
            data: $("#frmContactoProveedor").serialize(),
            cache: false,
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $(".x_content2").html(response);
                    $("#myContactoProveedor").modal("hide");
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

    $("#myImpuestoProveedor .btn-primary").unbind().bind("click", function (ev) {
        if ($("#Id").val() > 0 && $("#IdProveedor").val() > 0 && $("#IdImpuesto").val() > 0) {
            $.ajax({
                type: "POST",
                url: _root + $("#frmImpuestoProveedor").attr("action"),
                data: {
                    Id: $("#Id").val(),
                    IdProveedor: $("#IdProveedor").val(),
                    IdImpuesto: $("#IdImpuesto").val()
                },
                cache: false,
                success: function (response) {
                    if (response.indexOf("Error de Sistema") >= 0)
                        _setErrorMessage(response);
                    else {
                        $(".x_content3").html(response);
                        $("#myImpuestoProveedor").modal("hide");
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
        else {
            _setErrorMessage("Falta seleccionar un campo");
        }
    });

    showCascadeUbigeo("#IdPais", "#IdDepartamento", "#IdProvincia", "#IdDistrito");

    $("#IdTipoPersona").unbind().bind("change", function (ev) {
        if ($(this).val() == "36") {
            $("#ApellidoPaterno").removeAttr('readonly');
            $("#ApellidoMaterno").removeAttr('readonly');
            $("#Nombres").removeAttr('readonly');
        } else {
            $("#ApellidoPaterno").attr('readonly', 'readonly');
            $("#ApellidoMaterno").attr('readonly', 'readonly');
            $("#Nombres").attr('readonly', 'readonly');
        }
    });

    showCascade("#ddlTipoImpuesto", "ImpuestosxTipo", "#ddlImpuesto", "Impuesto");

    showCascade("#ddlPais", "BancosxPais", "#ddlBanco", "Banco");

}

/*******************************Pedidos******************************/

function _formEditPedido() {
    _formBack();

    _formDetailExtend("Pedido");

    showCascade2("#IdEmpresa", "SucusalesxEmpresa", "#IdSucursal", "Sucursal", "AreaSolicitantexEmpresa", "#IdAreaSolicitante", "Area Solicitante")

    $("button[name=btnAgregar]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#IdProducto").val() > 0) {
                if ($("#txtCantidad").val() > 0) {
                    $.ajax({
                        type: "POST",
                        url: "/DetallePedido/New",
                        data: { Id: 0, IdPedido: $("#Id").val(), IdProducto: $("#IdProducto").val(), Cantidad: $("#txtCantidad").val(), Precio: 0, Total: 0, Observaciones: $("#DescObservaciones").val() },
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
                }
                else {
                    _setErrorMessage("La Cantidad ingresada es incorrecta");
                }
            } else {
                _setErrorMessage("Debe de seleccionar un Producto");
            }
        } else {
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

    _formDetailDeleteGroupExtend("DetallePedido");

    $("#myDetallePedido .btn-primary").unbind().bind("click", function (ev) {
        $.ajax({
            type: "POST",
            url: _root + $("#frmDetallePedido").attr("action"),
            data: $("#frmDetallePedido").serialize(),
            cache: false,
            success: function (response) {
                if (response.indexOf("Error de Sistema") >= 0)
                    _setErrorMessage(response);
                else {
                    $(".x_content").html(response);
                    $("#myDetallePedido").modal("hide");
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

function _formEditDetallePedido() {
    selected = [];

    var table = $("#datatable-responsive-dp").DataTable({
        columns: [null, null, null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formDetailDeleteExtend("DetallePedido");
            _formEditDeleteExtendDetail("#datatable-responsive-dp", "DetallePedido");
            _formDetailViewExtend("DetallePedido");
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formDetailDeleteExtend("DetallePedido");
        _formEditDeleteExtendDetail("#datatable-responsive-dp", "DetallePedido");
        _formDetailViewExtend("DetallePedido");
    });

    _selectBehaviourExtend("#datatable-responsive-dp", selected);

}

/****************************Validar Pedido ***************************/
function _formValidaPedido() {
    selected = [];

    var table = $("#datatable-responsive").DataTable({
        select: _select,
        language: _language,
        sorting: false,
        drawCallback: function (settings) {
            _formEditDeleteExtend("ValPedido");
            _formActionCustomAdditional(".apro-item", "/ValPedido/Aprobar", "/ValPedido/Index");
            _formActionCustomAdditional(".rech-item", "/ValPedido/Rechazar", "/ValPedido/Index");
            _formActionCustomAdditional(".obs-item", "/ValPedido/Observar", "/ValPedido/Index");
            _formLinkInternal(".view-coti");
            _formLinkInternal(".view-oc");
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDeleteExtend("ValPedido");
        _formActionCustomAdditional(".apro-item", "/ValPedido/Aprobar", "/ValPedido/Index");
        _formActionCustomAdditional(".rech-item", "/ValPedido/Rechazar", "/ValPedido/Index");
        _formActionCustomAdditional(".obs-item", "/ValPedido/Observar", "/ValPedido/Index");
        _formLinkInternal(".view-coti");
        _formLinkInternal(".view-oc");
    });

    _selectBehaviour();

}

/************************Gnerar Cotizacion******************/
function _formGeneraCotiza() {
    selected = [];

    var table = $("#datatable-responsive").DataTable({
        select: _select,
        language: _language,
        sorting: false,
        drawCallback: function (settings) {
            _formEditDeleteExtend("GenCotiza");
            _formLinkInternal(".comp-item");
            _formLinkInternal(".view-coti");
            _formLinkInternal(".view-oc");
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDeleteExtend("GenCotiza");
        _formLinkInternal(".comp-item");
        _formLinkInternal(".view-coti");
        _formLinkInternal(".view-oc");
    });

    _selectBehaviour();

}

function _formGridGeneraCotizacion()
{
    $("#datatable-responsive-dp").DataTable({
        language: _language,
        paging: false,
        searching: false,
        sorting: false,
        autoWidth: false,
        columns: [{ width: "5%" }, { width: "25%" }, { width: "40%" }, { width: "5%" }, { width: "10%" }, { width: "5%" }, { width: "5%" }]
    });

    $("#myDeleteExtend .btn-primary").unbind().bind("click", function (ev) {
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
    });

    $("button[name=btnAgregar]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#IdProveedor").val() > 0) {
                $.ajax({
                    type: "POST",
                    url: "/GenCotiza/Cotizar",
                    data: { Id: $("#IdProveedor").val(), IdPadre: $("#Id").val() },
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
                _setErrorMessage("Debe de seleccionar un Proveedor");
            }
        } else {
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });
}

function _formGridGeneraCotDetail()
{

    var table = $("#datatable-responsive").DataTable({
        columns: [null, null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formDetailDeleteExtend("GenCotiza");
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formDetailDeleteExtend("GenCotiza");
    });

}

/******************************Cotizacion**********************************/

function _formEditCotizacion() {
    _formBack();

    _formDetailExtend("Cotizacion");

    $("#txtPrecio").on("input", function (ev) {
        var cant = (parseInt($("#txtCantidad").val()) == NaN ? 0 : parseInt($("#txtCantidad").val()));
        var precio = (parseFloat($(this).val()) == NaN ? 0 : parseFloat($(this).val()));
        $("#DescTotal").val(parseFloat(cant * precio).toFixed(3));
    });

    $("#txtCantidad").on("input", function (ev) {
        var precio = (parseFloat($("#txtPrecio").val()) == NaN ? 0 : parseFloat($("#txtPrecio").val()));
        var cant = (parseInt($(this).val()) == NaN ? 0 : parseInt($(this).val()));
        $("#DescTotal").val(parseFloat(cant * precio).toFixed(3));
    });

    $("button[name=btnAgregar]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#IdProducto").val() > 0) {
                if ($("#txtCantidad").val() > 0 && $("#txtCantidad").val().match(/^\d+$/)) {
                    if ($("#txtPrecio").val() > 0 && !(parseFloat($("#txtPrecio").val()) == NaN)) {
                        $.ajax({
                            type: "POST",
                            url: "/DetalleCotizacion/New",
                            data: {
                                Id: 0,
                                IdCotizacion: $("#Id").val(),
                                IdProducto: $("#IdProducto").val(),
                                IdTarifario: $("#IdTarifa").val(),
                                Cantidad: $("#txtCantidad").val(),
                                Precio: $("#txtPrecio").val(),
                                Total: 0,
                                Observacion: $("#DescObservaciones").val()
                            },
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
                        _setErrorMessage("El Precio de lista es incorrecto");
                    }
                }
                else {
                    _setErrorMessage("La Cantidad ingresada es incorrecta");
                }
            } else {
                _setErrorMessage("Debe de seleccionar un Producto");
            }
        } else {
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

    _formDetailDeleteGroupExtend("DetalleCotizacion");

    $("#myDetalleCotizacion .btn-primary").unbind().bind("click", function (ev) {
        if ($("#Cantidad").val() > 0 && $("#Cantidad").val().match(/^\d+$/)) {
            if ($("#Precio").val() > 0 && !(parseFloat($("#Precio").val()) == NaN)) {
                $.ajax({
                    type: "POST",
                    url: _root + $("#frmDetalleCotizacion").attr("action"),
                    data: $("#frmDetalleCotizacion").serialize(),
                    cache: false,
                    success: function (response) {
                        if (response.indexOf("Error de Sistema") >= 0)
                            _setErrorMessage(response);
                        else {
                            $(".x_content").html(response);
                            $("#myDetalleCotizacion").modal("hide");
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
                _setErrorMessage("El Precio de lista es incorrecto");
            }
        }
        else {
            _setErrorMessage("La Cantidad ingresada es incorrecta");
        }
    });

    _formActionCustomDetail(".x_content", "#IdProveedor");

    $(".aju-group").unbind().bind("click", function (ev) {
        ev.stopPropagation();
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            $("#myActionDetail .btn-primary").attr("data-val-url", $(this).attr("data-val-url"));
            $("#myActionDetail").modal("show");
        } else {
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

}

function _formEditDetalleCotizacion() {
    selected = [];

    var table = $("#datatable-responsive-dp").DataTable({
        columns: [null, null, null, null, null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formDetailDeleteExtend("DetalleCotizacion");
            _formEditDeleteExtendDetail("#datatable-responsive-dp", "DetalleCotizacion");
            _formDetailViewExtend("DetalleCotizacion");
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formDetailDeleteExtend("DetalleCotizacion");
        _formEditDeleteExtendDetail("#datatable-responsive-dp", "DetalleCotizacion");
        _formDetailViewExtend("DetalleCotizacion");
    });

    _selectBehaviourExtend("#datatable-responsive-dp", selected);

}

/****************************************Orden Compra*********************************************/

function _formEditOrdenCompra() {
    _formBack();

    _formDetailExtend("OrdenCompra");

    $("#txtPrecio").on("input", function (ev) {
        var cant = (parseInt($("#txtCantidad").val()) == NaN ? 0 : parseInt($("#txtCantidad").val()));
        var precio = (parseFloat($(this).val()) == NaN ? 0 : parseFloat($(this).val()));
        $("#DescTotal").val(parseFloat(cant * precio).toFixed(3));
    });

    $("#txtCantidad").on("input", function (ev) {
        var precio = (parseFloat($("#txtPrecio").val()) == NaN ? 0 : parseFloat($("#txtPrecio").val()));
        var cant = (parseInt($(this).val()) == NaN ? 0 : parseInt($(this).val()));
        $("#DescTotal").val(parseFloat(cant * precio).toFixed(3));
    });

    $("button[name=btnAgregar]").click(function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            if ($("#IdProducto").val() > 0) {
                if ($("#txtCantidad").val() > 0 && $("#txtCantidad").val().match(/^\d+$/)) {
                    if ($("#txtPrecio").val() > 0 && !(parseFloat($("#txtPrecio").val()) == NaN)) {
                        $.ajax({
                            type: "POST",
                            url: "/DetalleOrdenCompra/New",
                            data: {
                                Id: 0,
                                IdOrdenCompra: $("#Id").val(),
                                IdProducto: $("#IdProducto").val(),
                                Cantidad: $("#txtCantidad").val(),
                                Precio: $("#txtPrecio").val(),
                                Total: 0,
                                Observacion: $("#DescObservaciones").val()
                            },
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
                        _setErrorMessage("El Precio de lista es incorrecto");
                    }
                }
                else {
                    _setErrorMessage("La Cantidad ingresada es incorrecta");
                }
            } else {
                _setErrorMessage("Debe de seleccionar un Producto");
            }
        } else {
            _setErrorMessage("Antes de continuar debe guardar los datos de la cabecera");
        }
    });

    _formDetailDeleteGroupExtend("DetalleOrdenCompra");

    $("#myDetalleOrdenCompra .btn-primary").unbind().bind("click", function (ev) {
        if ($("#Cantidad").val() > 0 && $("#Cantidad").val().match(/^\d+$/)) {
            if ($("#Precio").val() > 0 && !(parseFloat($("#Precio").val()) == NaN)) {
                $.ajax({
                    type: "POST",
                    url: _root + $("#frmDetalleOrdenCompra").attr("action"),
                    data: $("#frmDetalleOrdenCompra").serialize(),
                    cache: false,
                    success: function (response) {
                        if (response.indexOf("Error de Sistema") >= 0)
                            _setErrorMessage(response);
                        else {
                            $(".x_content").html(response);
                            $("#myDetalleOrdenCompra").modal("hide");
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
                _setErrorMessage("El Precio de lista es incorrecto");
            }
        }
        else {
            _setErrorMessage("La Cantidad ingresada es incorrecta");
        }
    });

    _formActionCustomDetail(".x_content", "#IdProveedor");

}

function _formEditDetalleOrdenCompra() {
    selected = [];

    var table = $("#datatable-responsive-dp").DataTable({
        columns: [null, null, null, null, null, { orderable: false }],
        select: _select,
        language: _language,
        drawCallback: function (settings) {
            _formDetailDeleteExtend("DetalleOrdenCompra");
            _formEditDeleteExtendDetail("#datatable-responsive-dp", "DetalleOrdenCompra");
            _formDetailViewExtend("DetalleOrdenCompra");
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formDetailDeleteExtend("DetalleOrdenCompra");
        _formEditDeleteExtendDetail("#datatable-responsive-dp", "DetalleOrdenCompra");
        _formDetailViewExtend("DetalleOrdenCompra");
    });

    _selectBehaviourExtend("#datatable-responsive-dp", selected);
}

function _formFacturarOrdenCompra()
{
    $("button[name=btnGrabar]").unbind().bind("click", function (ev) {
        ev.preventDefault();
        if ($("#Id").val() > 0) {
            $.ajax({
                type: "POST",
                url: _root + "/OrdenCompra/Facturar",
                data: $("#frmOrdenCompra").serialize(),
                cache: false,
                success: function (response) {
                    if (response.indexOf("Error de Sistema") >= 0)
                        _setErrorMessage(response);
                    else {
                        _setSuccessMessage("Registro(s) modificado(s) satisfactoriamente");
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
        } else {
            _setErrorMessage("No se enuentra el código de la Orden de Compra");
        }
    });

    _setUploadControl(".x_content2", ".dropzone", "/Upload/OrdenCompra", "/OrdenCompraDoc/Delete", "/OrdenCompraDoc/ViewDocs", 5, 5, [_mimeDOC, _mimeDOCX, _mimeXLS, _mimeXLSX, _mimePDF], "/Upload/DownloadOrdenCompra/");
}

/****************************Validar Orden de Compra ***************************/
function _formValidaOrdenCompra() {
    selected = [];

    var table = $("#datatable-responsive").DataTable({
        select: _select,
        language: _language,
        sorting: false,
        drawCallback: function (settings) {
            _formEditDeleteExtend("ValOrdenCompra");
            _formActionCustomAdditional(".apro-item", "/ValOrdenCompra/Aprobar", "/ValOrdenCompra/Index");
            _formActionCustomAdditional(".rech-item", "/ValOrdenCompra/Rechazar", "/ValOrdenCompra/Index");
            _formActionCustomAdditional(".obs-item", "/ValOrdenCompra/Observar", "/ValOrdenCompra/Index");
        }
    });

    table.on('responsive-display', function (e, datatable, row, showHide, update) {
        _formEditDeleteExtend("ValOrdenCompra");
        _formActionCustomAdditional(".apro-item", "/ValOrdenCompra/Aprobar", "/ValOrdenCompra/Index");
        _formActionCustomAdditional(".rech-item", "/ValOrdenCompra/Rechazar", "/ValOrdenCompra/Index");
        _formActionCustomAdditional(".obs-item", "/ValOrdenCompra/Observar", "/ValOrdenCompra/Index");
    });

    _selectBehaviour();

}
