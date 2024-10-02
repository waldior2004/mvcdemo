using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System.Data;
using System;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.IO;

namespace com.msc.infraestructure.biz
{
    public class CargaLiquiCBL
    {
        private Repository _repositorio;

        public CargaLiquiCBL()
        {
            _repositorio = new Repository();
        }

        public Documento ObtCargaLiquiCDocNombre(int Id)
        {
            return _repositorio.ObtCargaLiquiCDocNombre(Id);
        }

        public List<CargaLiquiC> ObtCargaLiquiC()
        {
            return _repositorio.ObtCargaLiquiC();
        }

        public CargaLiquiC ObtCargaLiquiC(int Id)
        {
            return _repositorio.ObtCargaLiquiC(Id);
        }

        public List<CargaLiquiC> ObtEnviadosC()
        {
            return _repositorio.ObtEnviadosC();
        }

        public Respuesta EnviarCargaLiquiC(string Id, string Correo, string Tipo, string Comentario)
        {
            try
            {
                string nombreTerminal = string.Empty;
                List<string> lstCorreos = new List<string>();

                if (Tipo == "Enviado")
                    lstCorreos = (from p in _repositorio.ObtTablaGrupo("010")
                                  select p.Descripcion).ToList();
                else
                    lstCorreos = (from p in _repositorio.ObtTablaGrupo("011")
                                  select p.Descripcion).ToList();

                lstCorreos.Add(Correo);

                var objCargaLiquiC = _repositorio.ObtCargaLiquiC(Convert.ToInt32(Id));

                /*
                 * 
                 * ENRIQUE
                 * 
                 * */
                try
                {
                    var externoUser = _repositorio.ObtExterno(objCargaLiquiC.Usuario);
                    if (externoUser != null)
                    {
                        nombreTerminal = externoUser.Terminal.Descripcion;
                        lstCorreos.Add(externoUser.Email1);
                        lstCorreos.Add(externoUser.Email2);
                    }

                }
                catch (Exception ex)
                {
                    //TODO
                }

                /***
                 * 
                 * FIN ENRIQUE
                 */

                if (Tipo == "Aprobado")
                    objCargaLiquiC.Provision = _repositorio.GeneraCodigoProvision();

                objCargaLiquiC.Estado = Tipo;
                objCargaLiquiC.Comentario = Comentario;
                objCargaLiquiC.FecValida = DateTime.Now;
                var objResp = _repositorio.EditCargaLiquiC(objCargaLiquiC);

                if (Tipo == "Enviado")
                    Mailing.SendEnviarCargaLiquidacion(lstCorreos, objCargaLiquiC, nombreTerminal);
                else
                    Mailing.SendValidarCargaLiquidacion(lstCorreos, objCargaLiquiC);

                return objResp;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return MyException.OnException(ex);
            }
        }

        public void EditCargaLiquiC(CargaLiquiC obj)
        {
            try
            {
                var idDoc = obj.Id;
                obj.IdDocumento = idDoc;
                obj.Id = 0;
                obj.Procesados = 0;
                obj.Correctos = 0;
                obj.Errados = 0;
                obj.Estado = "Registrando ...";
                short errores = 0;
                var desc_errores = "";
                short procesados = 0;
                DateTime fechaZarpe = DateTime.Now;
                var viaje = "";
                var nave = "";
                var objNav001 = _repositorio.ObtNavexId(obj.IdNave);
                /*
                 *ENRIQUE 
                 */
                //
                string movimiento = obj.TipoEnvio == 0 ? "I" : "E";
                var validacion = _repositorio.ObtMasterDataItinerario(obj.IdNave, obj.IdViaje, obj.IdPuerto, movimiento, obj.IdViaje);
                if (validacion != null)
                {

                }
                else
                {

                }

                /**
                 * 
                 * FIN ENRIQUE
                 */
                if (objNav001 != null)
                    nave = objNav001.Descripcion;
                viaje = obj.IdViaje;

                Respuesta _resp = new Respuesta { Id = -2, Descripcion = "" };

                var objDocRet = _repositorio.ObtDocumento(idDoc);
                var ruta_excel = objDocRet.RutaLocal;
                var extension = objDocRet.Extension;
                CargaLiquiD objDetalle;

                if (extension.ToUpper() == ".EDI")
                {
                    string line;
                    int counter = 1;
                    var seq = "";
                    var move = "";
                    var function = "";
                    //var viaje = "";
                    //var nave = "";
                    var container = "";
                    var booking = "";
                    var linea = "MSC";
                    var cliente = "FRUITXCHANGE S.A.C";
                    var tipocarga = "FRESH ONIONS";

                    using (StreamReader file = new StreamReader(ruta_excel))
                    {

                        var fecha = "";

                        while ((line = file.ReadLine()) != null)
                        {
                            if (counter == 3)
                            {
                                var data = line.Split('+');
                                move = (data[1] == "34" ? "IN" : (data[1] == "36" ? "OUT" : ""));
                                seq = data[2];
                                function = data[3];
                            }
                            //else if (counter == 4)
                            //{
                            //    var data = line.Split('+');
                            //    viaje = data[2];
                            //    nave = data[8].Split(':')[3].Replace("'", "");
                            //}
                            else if (counter == 8)
                            {
                                var data = line.Split('+');
                                container = data[2];
                            }
                            else if (counter == 9)
                            {
                                var data = line.Split(':');
                                booking = data[1].Replace("'", "");
                            }
                            else if (line.Contains("DTM+7:"))
                            {
                                var data = line.Split(':');
                                var anio = data[1].Substring(0, 4);
                                var mes = data[1].Substring(4, 2);
                                var dia = data[1].Substring(6, 2);
                                var hora = data[1].Substring(8, 2);
                                var minuto = data[1].Substring(10, 2);
                                var tt = "am";
                                if (Convert.ToInt16(hora) > 12)
                                {
                                    hora = (Convert.ToInt16(hora) - 12).ToString().PadLeft(2, '0');
                                    tt = "pm";
                                }
                                else if (Convert.ToInt16(hora) == 0)
                                {
                                    hora = "12";
                                }

                                fecha = string.Format("{0}/{1}/{2} {3}:{4}", dia, mes, anio, hora, minuto);
                            }

                            counter++;
                        }

                        objDetalle = new CargaLiquiD
                        {
                            Id = 0,
                            IdCargaLiquiC = 0,
                            Item = 1,
                            Booking = booking.ToClearString(),
                            NumContenedores = container.ToClearString(),
                            Linea = linea.ToClearString(),
                            InDate = (move == "IN" ? fecha.ToClearDateTimeString() : ""),
                            OutDate = (move == "OUT" ? fecha.ToClearDateTimeString() : ""),
                            Shipper = cliente.ToClearString(),
                            Commodity = tipocarga.ToClearString(),
                            Nave = nave,
                            Viaje = viaje,
                            Estado = "Registrado"
                        };

                        var _objDetalleExists = _repositorio.ObtCargaLiquiD(objDetalle);

                        if (_objDetalleExists == null)
                        {
                            _resp = _repositorio.EditCargaLiquiC(obj);
                            if (_resp.Id == 0)
                            {
                                objDetalle.IdCargaLiquiC = Convert.ToInt32(_resp.Metodo);
                                var _respDeta = _repositorio.EditCargaLiquiD(objDetalle);
                                if (_respDeta.Id != 0)
                                {
                                    errores++;
                                    desc_errores += "1";
                                }
                            }
                        }
                        else
                        {
                            var _cabeceraTemp = _repositorio.ObtCargaLiquiC(_objDetalleExists.IdCargaLiquiC);

                            _cabeceraTemp.IdDocumento2 = obj.IdDocumento;

                            _resp = _repositorio.EditCargaLiquiC(_cabeceraTemp);
                            _resp.Metodo = _objDetalleExists.IdCargaLiquiC.ToString();

                            if (_resp.Id == 0)
                            {
                                if (objDetalle.InDate != "" && objDetalle.InDate != null)
                                {
                                    _objDetalleExists.InDate = objDetalle.InDate;
                                }
                                else if (objDetalle.OutDate != "" && objDetalle.OutDate != null)
                                {
                                    _objDetalleExists.OutDate = objDetalle.OutDate;
                                }

                                var _respDeta = _repositorio.EditCargaLiquiD(_objDetalleExists);
                                if (_respDeta.Id != 0)
                                {
                                    errores++;
                                    desc_errores += "1";
                                }
                            }
                        }
                    }

                    var existeViaje = _repositorio.ObtViaje(obj.IdViaje);
                    //var existeNave = _repositorio.ObtNave(nave);
                    //if (existeViaje == null)
                    //{
                    //    errores++;
                    //    desc_errores += string.Format("El Viaje {0} no está registrado", viaje);
                    //}
                    //else 
                    if (existeViaje != null)
                    {
                        if (DateTime.Now > existeViaje.FechaZarpe)
                        {
                            errores++;
                            desc_errores += string.Format("Fecha de Zarpe es {0}", existeViaje.FechaZarpe.ToString("dd/MM/yyyy"));
                        }
                    }
                    else
                    {
                        errores++;
                        desc_errores += "La nave y el viaje no tienen registrada una fecha de zarpe";
                    }
                    //else if (existeNave == null)
                    //{
                    //    errores++;
                    //    desc_errores += string.Format("La Nave {0} no está registrada", nave);
                    //}

                }
                else if (extension.ToUpper() == ".XLS")
                {
                    _resp = _repositorio.EditCargaLiquiC(obj);

                    if (_resp.Id == 0)
                    {
                        DataTable dt = DirectoryUtil.LeerExcel(ruta_excel);
                        var numfila = 0;
                        using (dt)
                        {
                            //var nave = "";
                            //var viaje = "";

                            foreach (DataRow dr in dt.Rows)
                            {
                                numfila++;

                                //for (int colum = 0; colum < dt.Columns.Count; colum++)
                                //{
                                //    if (numfila == 5 && colum == 2)
                                //        nave = dr[colum].ToString().Trim().Replace("'", "").Replace("´", "");
                                //    else if (numfila == 6 && colum == 2)
                                //        viaje = dr[colum].ToString().Trim().Replace("'", "").Replace("´", "");
                                //}

                                if (numfila >= 14)
                                {
                                    if (!(dr[8].ToString().ToClearString() == "" || dr[8].ToString().ToClearString() == null))
                                    {
                                        objDetalle = new CargaLiquiD
                                        {
                                            Id = 0,
                                            IdCargaLiquiC = Convert.ToInt32(_resp.Metodo),
                                            Item = Convert.ToInt16(numfila + 2),
                                            Booking = dr[1].ToString().ToClearString(),
                                            NumContenedores = dr[2].ToString().ToClearString(),
                                            Linea = dr[3].ToString().ToClearString(),
                                            InDate = dr[6].ToString().ToClearDateTimeString(),
                                            OutDate = dr[7].ToString().ToClearDateTimeString(),
                                            Shipper = dr[8].ToString().ToClearString(),
                                            Commodity = dr[10].ToString().ToClearString(),
                                            Nave = nave,
                                            Viaje = viaje,
                                            Estado = "Registrado"
                                        };

                                        var _respDeta = _repositorio.EditCargaLiquiD(objDetalle);
                                        if (_respDeta.Id != 0)
                                        {
                                            errores++;
                                            desc_errores += (numfila + 2).ToString() + ",";
                                        }
                                    }
                                }
                            }

                            var existeViaje = _repositorio.ObtViaje(obj.IdViaje);
                            //var existeNave = _repositorio.ObtNave(nave);
                            //if (existeViaje == null)
                            //{
                            //    errores++;
                            //    desc_errores += string.Format("El Viaje {0} no está registrado", viaje);
                            //}
                            //else 
                            if (existeViaje != null)
                            {
                                fechaZarpe = existeViaje.FechaZarpe;
                                //48 horas de tolerancia
                              /*  if (DateTime.Now.Date > existeViaje.FechaZarpe.Date.AddHours(48))
                                {
                                    errores++;
                                    desc_errores += string.Format("Fecha de Zarpe es {0}", existeViaje.FechaZarpe.ToString("dd/MM/yyyy"));
                                }*/
                            }
                            else
                            {
                                errores++;
                                desc_errores += "La nave y el viaje no tienen registrada una fecha de zarpe";
                            }
                            //else if (existeNave == null)
                            //{
                            //    errores++;
                            //    desc_errores += string.Format("La Nave {0} no está registrada", nave);
                            //}
                        }
                    }
                }
                else
                {
                    errores++;
                    desc_errores += "Extensión del archivo no soportado (sólo se acepta .xls y .edi)";
                }


                if (errores > 0)
                {
                    _resp = _repositorio.EditStatusCargaLiquiC(new CargaLiquiC
                    {
                        Id = Convert.ToInt32(_resp.Metodo),
                        Errados = Convert.ToInt16(errores),
                        Procesados = 0,
                        Correctos = 0,
                        Estado = string.Format("Error {0}", desc_errores)
                    });
                }
                else
                {

                    decimal total_fact = 0;
                    var objCab = _repositorio.ObtCargaLiquiC(Convert.ToInt32(_resp.Metodo));
                    objCab.Estado = "Procesando...";
                    var _resp2 = _repositorio.EditCargaLiquiC(objCab);

                    var formatDate = "d/MM/yyyy HH:mm";
                    foreach (var item in objCab.CargaLiquiDs)
                    {
                        var estadoDet = "Error: ";
                        DateTime dateEntrada;
                        DateTime dateSalida;
                        KeyValuePair<int, decimal> tarifaHora = new KeyValuePair<int, decimal>(0, 0);
                        KeyValuePair<int, int> horasCondicion = new KeyValuePair<int, int>(0, 0);
                        KeyValuePair<int, int> horasExcepcion = new KeyValuePair<int, int>(0, 0);

                        procesados++;
                        // var existeCont = _repositorio.ObtContenedor(item.NumContenedores, obj.IdViaje, obj.IdPuerto.ToString(), obj.TipoEnvio.ToString());
                        var existeCont = validacion.Find(p => !string.IsNullOrEmpty(p.cnt) && p.cnt.Trim().ToUpper().Equals(item.NumContenedores.Trim().ToUpper()));

                        // var existeBook = _repositorio.ObtBooking(item.Booking);
                        //la columna booking del arhivo excel es variable tanto BOOKING COMO BL

                        var existeBookBL = validacion.Find(p =>
                       //si es IMPO, se valida el BL
                       (movimiento.Equals("I") &&
                        !string.IsNullOrEmpty(p.numbl) && p.numbl.Trim().ToUpper().Equals(item.Booking.Trim().ToUpper()))
                        ||
                        //SI es EXPO, se valida el numero de booking
                         (movimiento.Equals("E") &&
                        !string.IsNullOrEmpty(p.numbooking) && p.numbooking.Trim().ToUpper().Equals(item.Booking.Trim().ToUpper()))
                        );

                        // var existeCliente = _repositorio.ObtCliente(item.Shipper);
                        var existeCliente = validacion.Find(p =>
                        !string.IsNullOrEmpty(p.RucCliente) &&
                        p.RucCliente.ToUpper().Trim().Equals(item.Shipper.ToUpper().Trim()));


                        var existeContenedorCliente = validacion.Find(p =>
                             !string.IsNullOrEmpty(p.cnt) && !string.IsNullOrEmpty(p.RucCliente) &&
                         p.cnt.Trim().ToUpper().Equals(item.NumContenedores.Trim().ToUpper()) &&
                        p.RucCliente.ToUpper().Trim().Equals(item.Shipper.ToUpper().Trim()));

                        /*
                        var existeContenedorClienteTipoCarga = validacion.Find(p =>
                         !string.IsNullOrEmpty(item.Commodity) &&
                           !string.IsNullOrEmpty(p.NombreProducto) && !string.IsNullOrEmpty(p.cnt) && !string.IsNullOrEmpty(p.RucCliente) &&
                           p.NombreProducto.Trim().ToUpper().Equals(item.Commodity.Trim().ToUpper()) &&
                         p.cnt.Trim().ToUpper().Equals(item.NumContenedores.Trim().ToUpper()) &&
                        p.RucCliente.ToUpper().Trim().Equals(item.Shipper.ToUpper().Trim()));
                        

                        Tabla existeTipoCarga = null;
                        if (existeContenedorClienteTipoCarga != null)
                        {
                            existeTipoCarga = _repositorio.ObtTabla(item.Commodity);

                        }
                        */

                        Tabla existeTipoCarga = _repositorio.ObtTabla(item.Commodity);

                        if (existeCont == null)
                        {
                            errores++;
                            estadoDet += "Contenedor Incorrecto";
                        }
                        else if (existeBookBL == null)
                        {
                            errores++;
                            estadoDet += "Número de Bill of Lading / Booking Incorrecto";
                        }

                        else if (existeCliente == null)
                        {
                            errores++;
                            estadoDet += "Cliente Incorrecto";
                        }
                        else if (existeContenedorCliente == null)
                        {
                            errores++;
                            estadoDet += "No existe el contenedor para el cliente en el viaje";
                        }
                        /* else if (existeContenedorClienteTipoCarga == null)
                         {

                             errores++;
                             estadoDet += "Cargamento y Contenedor incorrectos.";

                         }*/
                        else if (existeTipoCarga == null)
                        {
                            errores++;
                            estadoDet += "No existe el cargamento " + item.Commodity;
                        }
                        /* else if (item.Linea == string.Empty || item.Linea == null)
                         {
                             errores++;
                             estadoDet += "Línea Obligatorio";
                         }*/
                        else if (!DateTime.TryParseExact(item.InDate, formatDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateEntrada))
                        {
                            errores++;
                            estadoDet += "Fecha de Entrada Incorrecto (sólo se acepta d/MM/yyyy HH:mm)";
                        }
                        else if (!DateTime.TryParseExact(item.OutDate, formatDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateSalida))
                        {
                            errores++;
                            estadoDet += "Fecha de Salida Incorrecto (sólo se acepta d/MM/yyyy HH:mm)";
                        }
                        else if ((dateSalida - dateEntrada).TotalHours <= 0)
                        {
                            errores++;
                            estadoDet += "GATE IN no puede ser mayor a GATE OUT";
                        }
                        else if (dateSalida.Date > fechaZarpe.Date)
                        {
                            errores++;
                            estadoDet += string.Format("Fecha GATE OUT no debe ser mayor a la fecha de zarpe de la nave {0}", fechaZarpe.ToShortDateString());
                        }
                        else
                        {
                            tarifaHora = _repositorio.ObtTarifaCE(new Range<DateTime>(dateEntrada, dateSalida), objCab.IdTerminal);
                            if (tarifaHora.Key <= 0)
                            {
                                errores++;
                                estadoDet += "La Terminal no tiene una tarifa aprobada válida";
                            }
                            else
                            {

                                horasCondicion = _repositorio.ObtCondEspeCli(new Range<DateTime>(dateEntrada, dateSalida), existeCliente.IdCliente.Value, existeTipoCarga.Id, objCab.IdTerminal, item.Id);

                                horasExcepcion = _repositorio.ObtCondExcepcional(new Range<DateTime>(dateEntrada, dateSalida), obj.IdNave, obj.IdViaje, objCab.IdTerminal);

                                if (horasExcepcion.Key > 0)
                                    horasCondicion = horasExcepcion;

                                var htot = Convert.ToInt16((dateSalida - dateEntrada).TotalHours);
                                var hcond = Convert.ToInt16(horasCondicion.Value);

                                if (horasCondicion.Key <= 0)
                                {
                                    errores++;
                                    estadoDet += "No se tiene una condición especial para el cliente registrada";
                                }
                                else
                                {

                                    var _resp3 = _repositorio.EditCargaLiqui(new CargaLiqui
                                    {
                                        IdCargaLiquiD = item.Id,
                                        IdContenedor = existeCont.cnt.ToUpper().Trim(),
                                        IdBooking = existeBookBL.idbls,
                                        IdCliente = existeCliente.IdCliente.Value,
                                        IdTipoCarga = existeTipoCarga.Id,
                                        IdNave = obj.IdNave,
                                        IdViaje = obj.IdViaje,
                                        Linea = item.Linea,
                                        FecEntrada = dateEntrada,
                                        FecSalida = dateSalida,
                                        HorasTotal = htot,
                                        HorasReal = (hcond >= htot ? htot : hcond),
                                        TarifaHora = tarifaHora.Value,
                                        IdTarifa = tarifaHora.Key,
                                        IdCondEspeCli = horasCondicion.Key,
                                        Total = tarifaHora.Value * (hcond >= htot ? htot : hcond)
                                    });

                                    if (_resp3.Id != 0)
                                    {
                                        errores++;
                                        estadoDet += "No se pudo registrar el detalle en la BD";
                                    }
                                    else
                                    {
                                        estadoDet = "Registro OK";
                                        total_fact += tarifaHora.Value * (hcond >= htot ? htot : hcond);
                                    }

                                    if (tarifaHora.Key > 0 && horasCondicion.Key > 0)
                                        item.Total = tarifaHora.Value * (hcond >= htot ? htot : hcond);
                                }
                            }
                        }

                        item.Estado = estadoDet;

                        var _resp4 = _repositorio.EditCargaLiquiD(item);
                    }

                    objCab.Procesados = procesados;
                    objCab.Errados = errores;
                    objCab.Correctos = (short)(procesados - errores);
                    objCab.Estado = "Terminado";
                    objCab.Total = total_fact;
                    _resp2 = _repositorio.EditCargaLiquiC(objCab);

                }

            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, obj);
            }
        }

        public Respuesta ElimCargaLiquiC(int Id)
        {
            return _repositorio.ElimCargaLiquiC(Id);
        }
    }
}
