using com.msc.infraestructure.entities;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Linq;

namespace com.msc.infraestructure.utils
{
    public static class Mailing
    {
        public static void SendEnviarOrdenCompraProveedores(List<string> ToAddress, OrdenCompra obj)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Envío de Orden de Compra</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha registrado la orden de compra: </th></tr><tr><th style=\"width: 25%; text-align: left;\">Código Orden de Compra:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Proveedor:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Código Cotización:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Fecha Registro:</th><td style=\"text-align: left;\">{3}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Número de Artículos:</th><td style=\"text-align: left;\">{4}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Observación:</th><td style=\"text-align: left;\">{5}</td></tr></table>",
                obj.Codigo,
                obj.Proveedor.RazonSocial,
                obj.Cotizacion.Codigo,
                obj.FechaRegistro.ToString("dd/MM/yyyy"),
                obj.DetalleOrdenCompras.Count,
                obj.Observacion));

            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th style=\"width: 20%; text-align: left;\">Producto</th><th style=\"width: 20%; text-align: left;\">Unidad de Medida</th><th style=\"width: 20%; text-align: right;\">Cantidad</th><th style=\"width: 20%; text-align: right;\">Precio (USD)</th><th style=\"width: 20%; text-align: right;\">Total</th></tr>"));
            foreach (var item in obj.DetalleOrdenCompras)
            {
                strBody.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td style=\"text-align: right;\">{2}</td><td style=\"text-align: right;\">{3}</td><td style=\"text-align: right;\">{4}</td></tr>", item.Producto.Descripcion, item.Producto.UnidadMedida.Descripcion, item.Cantidad, item.Precio, item.Total));
            }
            strBody.Append(string.Format("<tr><td colspan=\"4\" style=\"text-align: right; font-weight: bold;\">Total</td><td style=\"text-align: right; font-weight: bold;\">{0}</td></tr>", obj.DetalleOrdenCompras.Sum(p => p.Total)));
            strBody.Append("</table><table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">Favor de contactar a su representante en MSC.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>");

            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Envío de Orden de Compra", strBody.ToString());
            }
        }
        public static void SendValidarOrdenCompra(List<string> ToAddress, OrdenCompra obj)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Validación de Solicitud de Orden de Compra</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha validado la solicitud de orden de compra: </th></tr><tr><th style=\"width: 25%; text-align: left;\">Código:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Cotización:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Proveedor:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Fecha Registro:</th><td style=\"text-align: left;\">{3}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Estado:</th><td style=\"text-align: left;\">{4}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Observación:</th><td style=\"text-align: left;\">{5}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Comentario Aprobador:</th><td style=\"text-align: left;\">{6}</td></tr> <tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">Favor de ingresar al sistema para su respectiva revisión.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>",
                obj.Codigo,
                obj.Cotizacion.Codigo,
                obj.Proveedor.RazonSocial,
                obj.FechaRegistro.ToString("dd/MM/yyyy"),
                obj.Estado.Descripcion,
                obj.Observacion,
                obj.ComentAprobador));

            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Validación de Solicitud de Orden de Compra", strBody.ToString());
            }
        }
        public static void SendRegistrarOrdenCompra(List<string> ToAddress, OrdenCompra obj)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Registro de Solicitud de Orden de Compra</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha registrado la solicitud de orden de compra: </th></tr><tr><th style=\"width: 25%; text-align: left;\">Código:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Cotización:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Proveedor:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Fecha Registro:</th><td style=\"text-align: left;\">{3}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Estado:</th><td style=\"text-align: left;\">{4}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Observación:</th><td style=\"text-align: left;\">{5}</td></tr> <tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">Favor de ingresar al sistema para su respectiva revisión.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>",
                obj.Codigo,
                obj.Cotizacion.Codigo,
                obj.Proveedor.RazonSocial,
                obj.FechaRegistro.ToString("dd/MM/yyyy"),
                obj.Estado.Descripcion,
                obj.Observacion));

            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Registro de Solicitud de Orden de Compra", strBody.ToString());
            }
        }
        public static void SendEnviarCotizacionProveedores(List<string> ToAddress, Cotizacion obj)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Envío de Cotización</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha registrado la cotización: </th></tr><tr><th style=\"width: 25%; text-align: left;\">Código Cotización:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Proveedor:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Código Pedido:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Fecha Cotización:</th><td style=\"text-align: left;\">{3}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Número de Artículos:</th><td style=\"text-align: left;\">{4}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Observación:</th><td style=\"text-align: left;\">{5}</td></tr></table>",
                obj.Codigo,
                obj.Proveedor.RazonSocial,
                obj.Pedido.Codigo,
                obj.FechaCotizacion.ToString("dd/MM/yyyy"),
                obj.DetalleCotizaciones.Count,
                obj.Observacion));

            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th style=\"width: 20%; text-align: left;\">Producto</th><th style=\"width: 20%; text-align: left;\">Unidad de Medida</th><th style=\"width: 20%; text-align: right;\">Cantidad</th><th style=\"width: 20%; text-align: right;\">Precio (USD)</th><th style=\"width: 20%; text-align: right;\">Total</th></tr>"));
            foreach (var item in obj.DetalleCotizaciones)
            {
                strBody.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td style=\"text-align: right;\">{2}</td><td style=\"text-align: right;\">{3}</td><td style=\"text-align: right;\">{4}</td></tr>", item.Producto.Descripcion, item.Producto.UnidadMedida.Descripcion, item.Cantidad, item.Precio, item.Total));
            }

            strBody.Append(string.Format("<tr><td colspan=\"4\" style=\"text-align: right; font-weight: bold;\">Total</td><td style=\"text-align: right; font-weight: bold;\">{0}</td></tr>", obj.DetalleCotizaciones.Sum(p => p.Total)));
            strBody.Append("</table><table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">Favor de contactar a su representante en MSC.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>");

            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Envío de Cotización", strBody.ToString());
            }
        }
        public static void SendValidarPedido(List<string> ToAddress, Pedido obj)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Validación de Solicitud de Pedido</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha validado la solicitud de pedido: </th></tr><tr><th style=\"width: 25%; text-align: left;\">Código:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Empresa:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Sucursal:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Area Solicitante:</th><td style=\"text-align: left;\">{3}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Centro de Costo:</th><td style=\"text-align: left;\">{4}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Estado:</th><td style=\"text-align: left;\">{5}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Comentario Aprobador:</th><td style=\"text-align: left;\">{6}</td></tr> <tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">Favor de ingresar al sistema para su respectiva revisión.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>",
                obj.Codigo,
                obj.Empresa.Descripcion,
                obj.Sucursal.Descripcion,
                obj.AreaSolicitante.Descripcion,
                obj.CentroCosto.Descripcion,
                obj.Estado.Descripcion,
                obj.ComentAprobador));
            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Validación de Solicitud de Pedido", strBody.ToString());
            }
        }
        public static void SendRegistrarPedido(List<string> ToAddress, Pedido obj)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Registro de Solicitud de Pedido</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha registrado la solicitud de pedido: </th></tr><tr><th style=\"width: 25%; text-align: left;\">Código:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Empresa:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Sucursal:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Area Solicitante:</th><td style=\"text-align: left;\">{3}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Centro de Costo:</th><td style=\"text-align: left;\">{4}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Estado:</th><td style=\"text-align: left;\">{5}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Fecha Petición:</th><td style=\"text-align: left;\">{6}</td></tr> <tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">Favor de ingresar al sistema para su respectiva revisión.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>",
                obj.Codigo,
                obj.Empresa.Descripcion,
                obj.Sucursal.Descripcion,
                obj.AreaSolicitante.Descripcion,
                obj.CentroCosto.Descripcion,
                obj.Estado.Descripcion,
                obj.FechaPeticion.ToString("dd/MM/yyyy")));
            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Registro de Solicitud de Pedido", strBody.ToString());
            }
        }
        public static void SendValidarCargaLiquidacion(List<string> ToAddress, CargaLiquiC obj)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Validación de Archivo de Liquidación</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha actualizado el estado de su liquidación: </th></tr><tr><th style=\"width: 25%; text-align: left;\">Archivo:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Filas Procesadas:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Filas con Error:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Usuario Registro:</th><td style=\"text-align: left;\">{3}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Estado Aprobación:</th><td style=\"text-align: left;\">{4}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Código Provisión:</th><td style=\"text-align: left;\">{5}</td></tr> <tr><th style=\"width: 25%; text-align: left;\">Comentario:</th><td style=\"text-align: left;\">{6}</td></tr> <tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">Favor de ingresar al sistema para su respectiva revisión.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>",
                obj.Documento.Nombre,
                obj.Procesados,
                obj.Errados,
                obj.Usuario,
                obj.Estado,
                obj.Provision,
                obj.Comentario));
            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Validación de Archivo de Liquidación", strBody.ToString());
            }
        }

        public static void SendEnviarCargaLiquidacion(List<string> ToAddress, CargaLiquiC obj, string nombreTerminal)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Envío de Archivo de Liquidación</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha registrado la liquidación: </th></tr>" +
                "<tr><th style=\"width: 25%; text-align: left;\">Terminal:</th><td style=\"text-align: left;\">{4}</td></tr>" +
"<tr><th style =\"width: 25%; text-align: left;\">Nave:</th><td style=\"text-align: left;\">{5}</td></tr>" +
"<tr><th style =\"width: 25%; text-align: left;\">Viaje:</th><td style=\"text-align: left;\">{6}</td></tr>" +
"<tr><th style=\"width: 25%; text-align: left;\">Archivo:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Filas Procesadas:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Filas con Error:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Usuario Registro:</th><td style=\"text-align: left;\">{3}</td></tr><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">Favor de ingresar al sistema para su respectiva revisión.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>",
                obj.Documento.Nombre,
                obj.Procesados,
                obj.Errados,
                obj.Usuario, nombreTerminal, obj.Nave.Descripcion, obj.Viaje.Descripcion));
            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Envío de Archivo de Liquidación", strBody.ToString());
            }
        }

        public static void SendRegistroCondiciónEspecial(List<string> ToAddress, CondEspeCli obj)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Registro de Nueva Condición Especial</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha creado una nueva condición especial: </th></tr><tr><th style=\"width: 25%; text-align: left;\">Razón Social:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Nombre:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Fecha de Inicio:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"width: 25%; text-align: left;\">Fecha de Fin:</th><td style=\"text-align: left;\">{3}</td></tr><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">Favor de ingresar al sistema para su respectiva revisión.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>",
                obj.DescReferencia,
                obj.Nombre,
                obj.FechaInicio.ToString("dd/MM/yyyy"),
                obj.FechaFin.ToString("dd/MM/yyyy")));
            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Registro de Condición Especial", strBody.ToString());
            }
        }

        public static void SendAprobacionTarifaCE(List<string> ToAddress, TarifaCE obj)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Aprobación de Tarifa de Condición de Energía</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha validado la tarifa con las siguiente descripción:</th></tr><tr><th style=\"width: 25%; text-align: left;\">Terminal:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"text-align: left;\">Período:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"text-align: left;\">Moneda:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"text-align: left;\">Importe:</th><td style=\"text-align: left;\">{3}</td></tr><tr><th style=\"text-align: left;\">Fecha de Inicio:</th><td style=\"text-align: left;\">{4}</td></tr><tr><th style=\"text-align: left;\">Fecha de Fin:</th><td style=\"text-align: left;\">{5}</td></tr><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">La tarifa ha sido aprobada.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>",
                obj.Terminal.Descripcion,
                obj.PerTarifa.Descripcion,
                obj.Moneda.Descripcion,
                obj.Importe,
                obj.FechaInicio.ToString("dd/MM/yyyy"),
                obj.FechaFin.ToString("dd/MM/yyyy")));

            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Aprobación de Tarifa", strBody.ToString());
            }
        }

        public static void SendRegistroTarifaCE(List<string> ToAddress, TarifaCE obj)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Registro de Tarifa de Condición de Energía</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha creado la tarifa con las siguiente descripción:</th></tr><tr><th style=\"width: 25%; text-align: left;\">Terminal:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"text-align: left;\">Período:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"text-align: left;\">Moneda:</th><td style=\"text-align: left;\">{2}</td></tr><tr><th style=\"text-align: left;\">Importe:</th><td style=\"text-align: left;\">{3}</td></tr><tr><th style=\"text-align: left;\">Fecha de Inicio:</th><td style=\"text-align: left;\">{4}</td></tr><tr><th style=\"text-align: left;\">Fecha de Fin:</th><td style=\"text-align: left;\">{5}</td></tr><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px;\">Favor de ingresar al sistema para su respectiva aprobación.</th></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>",
                obj.Terminal.Descripcion,
                obj.PerTarifa.Descripcion,
                obj.Moneda.Descripcion,
                obj.Importe,
                obj.FechaInicio.ToString("dd/MM/yyyy"),
                obj.FechaFin.ToString("dd/MM/yyyy")));

            foreach (var item in ToAddress)
            {
                List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(item) };
                SendMail(lstMail, null, "Notificación de Registro de Tarifa", strBody.ToString());
            }
        }

        public static void SendNotificationResetKey(string ToAddress, string Usuario, string Clave)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Actualización de Clave</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha actualizado la clave del usuario</th></tr><tr><th style=\"width: 25%; text-align: left;\">Usuario:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"text-align: left;\">Clave:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>", Usuario, Clave));
            List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(ToAddress) };
            SendMail(lstMail, null, "Notificación de actualización de clave", strBody.ToString());
        }
        public static void SendNotificationCreateUser(string ToAddress, string Usuario, string Clave)
        {
            StringBuilder strBody = new StringBuilder();
            strBody.Append("<h3>Correo de Notificación de Generación de Clave</h3>");
            strBody.Append(string.Format("<table style=\"width: 100%; border-top: 1px solid yellow; font-family: Calibri;\"><tr><th colspan=\"2\" style=\"text-align: left; padding-bottom: 10px; padding-top: 10px;\">Se ha creado la cuenta del usuario</th></tr><tr><th style=\"width: 25%; text-align: left;\">Usuario:</th><td style=\"text-align: left;\">{0}</td></tr><tr><th style=\"text-align: left;\">Clave:</th><td style=\"text-align: left;\">{1}</td></tr><tr><th style=\"text-align: left; padding-bottom: 10px;\">Atte. Equipo de Desarrollo MSC</th></tr></table><br/><br/><p style=\"font-family: Calibri;\">Este correo es generado por un robot; no responda a este remitente.</p>", Usuario, Clave));
            List<MailAddress> lstMail = new List<MailAddress> { new MailAddress(ToAddress) };
            SendMail(lstMail, null, "Notificación de creación de nueva cuenta", strBody.ToString());
        }
        private static void SendMail(List<MailAddress> To, List<MailAddress> CC, string Subject, string Body)
        {
            try
            {
                SmtpClient sc = new SmtpClient();
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("from@example.com", "BotMail - Send Email Notification");
                foreach (var item in To)
                {
                    mm.To.Add(item);
                }
                if (CC != null)
                {
                    foreach (var item in CC)
                    {
                        mm.CC.Add(item);
                    }
                }
                mm.Subject = Subject;
                mm.Body = Body;
                mm.IsBodyHtml = true;
                mm.Priority = MailPriority.High;
                //sc.Send(mm);

                sc.SendCompleted += (s, e) =>
                {
                    sc.Dispose();
                    mm.Dispose();
                };
                sc.SendAsync(mm, null);

            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
            }
        }
    }
}
