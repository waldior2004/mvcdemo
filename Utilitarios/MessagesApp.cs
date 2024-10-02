using com.msc.infraestructure.entities;
using System.Web.Mvc;

namespace com.msc.infraestructure.utils
{
    public enum MessageCode
    {
        InsertOK = 1,
        UpdateOK = 2,
        DeleteOK = 3,
        NotFoundRecord = 4,
        InternalError = 5,
        AuthenticateOK = 6,
        AuthenticateBadPassword = 7,
        AuthenticateAccountError = 8,
        InvalidFields = 9,
        ExpirateSession = 10,
        UnAuthorizedRequest = 11,
        NotProfileFound = 12,
        AuthenticationBadDomain = 13,
        CodeDescAlreadyexists = 14,
        PasswordNotMatching = 15,
        ChangePasswordOK = 16,
        RangeDatesTarifaCE = 17,
        RangeDatesCondEspeCli = 18,
        TerminalYaExisteCondicion = 19,
        NumberDaysExceedCondition = 20,
        RangeDiasDetalleCondEsp = 21,
        NumberDaysAsignacionTrans = 22,
        TarifarioAlreadyexists = 23,
        RangeDatesTarifario = 24,
        MustSaveDataBeforeSend = 25,
        PedidoExistsCotizacion = 26,
        CotizacionDetalleErrores = 27,
        CotizacionExistsOC = 28,
        OrdenCompraExists = 29,
        OrdenCompraDetalleErrores = 30

    }

    public enum BitacoraActionCode
    {
        Insercion = 1,
        Actualizacion = 2,
        Eliminacion = 3,
        Consulta = 4
    }

    public enum BitacoraModeCode
    {
        Manual = 1,
        Automatico = 2
    }

    public static class MessagesApp
    {
        public static string BitacoraModeMessage(BitacoraModeCode action)
        {
            switch (action)
            {
                case BitacoraModeCode.Manual:
                    return "Manual";
                default:
                    return "Automático";
            }
        }

        public static string BitacoraActionMessage(BitacoraActionCode action)
        {
            switch (action) {
                case BitacoraActionCode.Insercion:
                    return "Inserción";
                case BitacoraActionCode.Actualizacion:
                    return "Actualización";
                case BitacoraActionCode.Eliminacion:
                    return "Eliminación";
                default:
                    return "Consulta";
            }
        }

        public static Respuesta BackAppMessage(MessageCode code, ModelStateDictionary dict = null)
        {
            var objR = new Respuesta();
            switch (code)
            {
                case MessageCode.InsertOK:
                    objR = new Respuesta { Id = 0, Descripcion = "Registro Insertado Con Éxito." };
                    break;
                case MessageCode.UpdateOK:
                    objR = new Respuesta { Id = 0, Descripcion = "Registro Actualizado con Éxito." };
                    break;
                case MessageCode.DeleteOK:
                    objR = new Respuesta { Id = 0, Descripcion = "Registro Eliminado con Éxito." };
                    break;
                case MessageCode.NotFoundRecord:
                    objR = new Respuesta { Id = 1, Descripcion = "Registro No Encontrado." };
                    break;
                case MessageCode.InternalError:
                    objR = new Respuesta { Id = -1, Descripcion = "Error interno, consulte al administrador." };
                    break;
                case MessageCode.AuthenticateOK:
                    objR = new Respuesta { Id = 0, Descripcion = "Autenticación Exitosa." };
                    break;
                case MessageCode.AuthenticateBadPassword:
                    objR = new Respuesta { Id = 1, Descripcion = "Contraseña Incorrecta." };
                    break;
                case MessageCode.AuthenticateAccountError:
                    objR = new Respuesta { Id = 1, Descripcion = "Cuenta no existe." };
                    break;
                case MessageCode.InvalidFields:
                    objR = new Respuesta { Id = 9, Descripcion = "Campos Inválidos." };
                    foreach (ModelState modelState in dict.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            objR.Message += string.Format("{0}<br/>", error.ErrorMessage);
                        }
                    }
                    break;
                case MessageCode.ExpirateSession:
                    objR = new Respuesta { Id = 1, Descripcion = "Sesión Expirada." };
                    break;
                case MessageCode.UnAuthorizedRequest:
                    objR = new Respuesta { Id = 1, Descripcion = "No está autorizado a realizar la acción solicitada." };
                    break;
                case MessageCode.NotProfileFound:
                    objR = new Respuesta { Id = 1, Descripcion = "No tiene un perfil asociado." };
                    break;
                case MessageCode.AuthenticationBadDomain:
                    objR = new Respuesta { Id = 1, Descripcion = "Error de autenticación en el dominio." };
                    break;
                case MessageCode.CodeDescAlreadyexists:
                    objR = new Respuesta { Id = 1, Descripcion = "El Código o Descripción Asignado ya existe." };
                    break;
                case MessageCode.TarifarioAlreadyexists:
                    objR = new Respuesta { Id = 1, Descripcion = "Ya existe un tariario para este proveedor en este periodo." };
                    break;
                case MessageCode.PasswordNotMatching:
                    objR = new Respuesta { Id = 1, Descripcion = "Las Contraseñas no coinciden." };
                    break;
                case MessageCode.ChangePasswordOK:
                    objR = new Respuesta { Id = 0, Descripcion = "Se ha realizado el cambio de contraseña." };
                    break;
                case MessageCode.RangeDatesTarifaCE:
                    objR = new Respuesta { Id = 1, Descripcion = "Los rangos de fecha de la tarifa se cruzan con otra tarifa, del mismo terminal y del mismo período." };
                    break;
                case MessageCode.RangeDatesCondEspeCli:
                    objR = new Respuesta { Id = 1, Descripcion = "Los rangos de fecha de la Condición Especial se cruzan con las de otra condición especial." };
                    break;
                case MessageCode.TerminalYaExisteCondicion:
                    objR = new Respuesta { Id = 1, Descripcion = "La terminal ya está asociada a la condición especial." };
                    break;
                case MessageCode.NumberDaysExceedCondition:
                    objR = new Respuesta { Id = 1, Descripcion = "El número de días excede el total asignado." };
                    break;
                case MessageCode.RangeDiasDetalleCondEsp:
                    objR = new Respuesta { Id = 1, Descripcion = "El rango de días colocado se cruza, con otra asignación." };
                    break;
                case MessageCode.NumberDaysAsignacionTrans:
                    objR = new Respuesta { Id = 1, Descripcion = "El número de días, entra en conflicto con la asignación diaria de transporte." };
                    break;
                case MessageCode.RangeDatesTarifario:
                    objR = new Respuesta { Id = 1, Descripcion = "Los rangos de fecha del tarifario de ese producto/proveedor se cruzan con las de otro tarifario." };
                    break;
                case MessageCode.MustSaveDataBeforeSend:
                    objR = new Respuesta { Id = 1, Descripcion = "Debe de Guardar los datos antes de continuar" };
                    break;
                case MessageCode.PedidoExistsCotizacion:
                    objR = new Respuesta { Id = 1, Descripcion = "El Proveedor ya está asociado a otra cotización" };
                    break;
                case MessageCode.CotizacionDetalleErrores:
                    objR = new Respuesta { Id = 1, Descripcion = "La Cotización tiene productos con precio y/o Cantidad incorrectos" };
                    break;
                case MessageCode.CotizacionExistsOC:
                    objR = new Respuesta { Id = 1, Descripcion = "La Cotización ya tiene una O/C asociada" };
                    break;
                case MessageCode.OrdenCompraExists:
                    objR = new Respuesta { Id = 1, Descripcion = "No se puede agregar/modificar o eliminar una cotización, debido a que ya se generó una O/C." };
                    break;
                case MessageCode.OrdenCompraDetalleErrores:
                    objR = new Respuesta { Id = 1, Descripcion = "La Orden de Compra tiene productos con precio y/o Cantidad incorrectos" };
                    break;
            }
            return objR;
        }
    }
}
