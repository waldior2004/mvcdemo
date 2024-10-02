using com.msc.infraestructure.biz;
using com.msc.infraestructure.entities;
using com.msc.infraestructure.entities.impresion;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.Collections.Generic;

namespace com.msc.services.implementations
{
    public partial class SistemaService : ISistema
    {
        private GrupoTablaBL _grupotablaLogic;
        private TablaBL _tablaLogic;
        private ExternoBL _externoLogic;
        private TarifaCEBL _tarifaceLogic;
        private TarifaCEDocBL _tarifacedocLogic;
        private ClienteBL _clienteLogic;
        private DocumentoBL _documentoLogic;
        private BitacoraBL _bitacoraLogic;
        private CondEspeCliBL _condespecliLogic;
        private CondEspeCliDetalleBL _condespeclidetalleLogic;
        private CondEspeCliDiaBL _condespeclidiaLogic;
        private CondEspeCliDocBL _condespeclidocLogic;
        private CargaLiquiCBL _cargaliquiCLogic;
        private CargaLiquiBL _cargaliquiLogic;
        private ProductoBL _productoLogic;
        private ProveedorBL _proveedorLogic;
        private TarifarioBL _tarifarioLogic;
        private FamiliaBL _FamiliaLogic;
        private SubFamiliaBL _SubFamiliaLogic;
        private SucursalBL _sucursalLogic;
        private TarifarioDocBL _tarifarioDocLogic;
        private DatoComercialBL _datocomercialLogic;
        private ContactoProveedorBL _contactoproveedorLogic;
        private NaveBL _naveLogic;
        private ViajeBL _viajeLogic;
        private PuertoBL _puertoLogic;
        private AreaSolicitanteBL _areasolLogic;
        private CentroCostoBL _centrocostoLogic;
        private PedidoBL _pedidoLogic;
        private DetallePedidoBL _detallepedidoLogic;
        private TempoPedidoBL _tempopedidoLogic;
        private CotizacionBL _cotizacionLogic;
        private DetalleCotizacionBL _detallecotizacionLogic;
        private OrdenCompraBL _ordencompraLogic;
        private DetalleOrdenCompraBL _detalleordencompraLogic;
        private OrdenCompraDocBL _ordencompradocLogic;
        private UbigeoBL _ubigeoLogic;
        private ImpuestoBL _impuestoLogic;
        private ImpuestoProveedorBL _impuestoproveedorLogic;
        private BancoBL _bancoLogic;
        private EmpresaBL _empresaLogic;
        private ProvisionBL _provisionLogic;
        private TareaBL _tareaLogic;

        public SistemaService()
        {
            _grupotablaLogic = new GrupoTablaBL();
            _tablaLogic = new TablaBL();
            _externoLogic = new ExternoBL();
            _tarifaceLogic = new TarifaCEBL();
            _tarifacedocLogic = new TarifaCEDocBL();
            _clienteLogic = new ClienteBL();
            _documentoLogic = new DocumentoBL();
            _bitacoraLogic = new BitacoraBL();
            _condespecliLogic = new CondEspeCliBL();
            _condespeclidetalleLogic = new CondEspeCliDetalleBL();
            _condespeclidocLogic = new CondEspeCliDocBL();
            _condespeclidiaLogic = new CondEspeCliDiaBL();
            _cargaliquiCLogic = new CargaLiquiCBL();
            _cargaliquiLogic = new CargaLiquiBL();
            _productoLogic = new ProductoBL();
            _proveedorLogic = new ProveedorBL();
            _tarifarioLogic = new TarifarioBL();
            _FamiliaLogic = new FamiliaBL();
            _SubFamiliaLogic = new SubFamiliaBL();
            _sucursalLogic = new SucursalBL();
            _tarifarioDocLogic = new TarifarioDocBL();
            _datocomercialLogic = new DatoComercialBL();
            _contactoproveedorLogic = new ContactoProveedorBL();
            _naveLogic = new NaveBL();
            _viajeLogic = new ViajeBL();
            _areasolLogic = new AreaSolicitanteBL();
            _centrocostoLogic = new CentroCostoBL();
            _pedidoLogic = new PedidoBL();
            _detallepedidoLogic = new DetallePedidoBL();
            _tempopedidoLogic = new TempoPedidoBL();
            _cotizacionLogic = new CotizacionBL();
            _detallecotizacionLogic = new DetalleCotizacionBL();
            _ordencompraLogic = new OrdenCompraBL();
            _detalleordencompraLogic = new DetalleOrdenCompraBL();
            _ordencompradocLogic = new OrdenCompraDocBL();
            _puertoLogic = new PuertoBL();
            _ubigeoLogic = new UbigeoBL();
            _impuestoLogic = new ImpuestoBL();
            _impuestoproveedorLogic = new ImpuestoProveedorBL();
            _bancoLogic = new BancoBL();
            _empresaLogic = new EmpresaBL();
            _provisionLogic = new ProvisionBL();
            _tareaLogic = new TareaBL();
        }

        #region Puerto
        public List<PuertoDTO> ObtPuerto()
        {
            var list = _puertoLogic.ObtPuerto();
            var lstR = new List<PuertoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetPuertoDTO());
            }
            return lstR;
        }
        #endregion

        #region Documento
        public RespuestaDTO EditDocumento(DocumentoDTO obj)
        {
            var objR = _documentoLogic.EditDocumento(obj.SetDocumento());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimDocumento(int Id)
        {
            var objR = _documentoLogic.ElimDocumento(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Viaje
        public List<ViajeDTO> ObtAllViaje(string desc)
        {
            var list = _viajeLogic.ObtAllViaje(desc);
            var lstR = new List<ViajeDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetViajeDTO());
            }
            return lstR;
        }
        public List<ViajeDTO> ObtViajexNave(string id, int port)
        {
            var list = _viajeLogic.ObtViajexNave(id, port);
            var lstR = new List<ViajeDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetViajeDTO());
            }
            return lstR;
        }
        #endregion

        #region Nave
        public List<NaveDTO> ObtAllNave(string desc)
        {
            var list = _naveLogic.ObtAllNave(desc);
            var lstR = new List<NaveDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetNaveDTO());
            }
            return lstR;
        }
        #endregion

        #region Cliente
        public List<ClienteDTO> ObtCliente()
        {
            var list = _clienteLogic.ObtCliente();
            var lstR = new List<ClienteDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetClienteDTO());
            }
            return lstR;
        }

        public List<ClienteDTO> ObtAllCliente(string desc)
        {
            var list = _clienteLogic.ObtAllCliente(desc);
            var lstR = new List<ClienteDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetClienteDTO());
            }
            return lstR;
        }
        #endregion

        #region GrupoTabla
        public List<GrupoTablaDTO> ObtGrupoTabla()
        {
            var list = _grupotablaLogic.ObtGrupoTabla();
            var lstR = new List<GrupoTablaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetGrupoTablaDTO());
            }
            return lstR;
        }

        public GrupoTablaDTO ObtGrupoTabla(int Id)
        {
            var objR = _grupotablaLogic.ObtGrupoTabla(Id);
            return objR.GetGrupoTablaDTO();
        }

        public RespuestaDTO EditGrupoTabla(GrupoTablaDTO obj)
        {
            var objR = _grupotablaLogic.EditGrupoTabla(obj.SetGrupoTabla());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimGrupoTabla(int Id)
        {
            var objR = _grupotablaLogic.ElimGrupoTabla(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Tabla
        public List<TablaDTO> ObtTabla()
        {
            var list = _tablaLogic.ObtTabla();
            var lstR = new List<TablaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetTablaDTO());
            }
            return lstR;
        }

        public List<TablaDTO> ObtTablaGrupo(string Codigo)
        {
            var list = _tablaLogic.ObtTablaGrupo(Codigo);
            var lstR = new List<TablaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetTablaDTO());
            }
            return lstR;
        }

        public TablaDTO ObtTabla(int Id)
        {
            var objR = _tablaLogic.ObtTabla(Id);
            return objR.GetTablaDTO();
        }

        public RespuestaDTO EditTabla(TablaDTO obj)
        {
            var objR = _tablaLogic.EditTabla(obj.SetTabla());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimTabla(int Id)
        {
            var objR = _tablaLogic.ElimTabla(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Externo
        public ExternoDTO ObtExternoPorUserName(string userName)
        {
            var objR = _externoLogic.ObtExterno(userName);
            return objR.GetExternoDTO();
        }


        public List<ExternoDTO> ObtExterno()
        {
            var list = _externoLogic.ObtExterno();
            var lstR = new List<ExternoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetExternoDTO());
            }
            return lstR;
        }

        public ExternoDTO ObtExterno(int Id)
        {
            var objR = _externoLogic.ObtExterno(Id);
            return objR.GetExternoDTO();
        }

        public RespuestaDTO ResetKeyExterno(int Id)
        {
            var objR = _externoLogic.ResetKeyExterno(Id);
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO EditExterno(ExternoDTO obj)
        {
            var objR = _externoLogic.EditExterno(obj.SetExterno());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimExterno(int Id)
        {
            var objR = _externoLogic.ElimExterno(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region TarifaCE
        public List<TarifaCEDTO> ObtTarifaCE()
        {
            var list = _tarifaceLogic.ObtTarifaCE();
            var lstR = new List<TarifaCEDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetTarifaCEDTO());
            }
            return lstR;
        }

        public List<TarifaCEDTO> ObtTarifaHistoricoCE()
        {
            var list = _tarifaceLogic.ObtTarifaHistoricoCE();
            var lstR = new List<TarifaCEDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetTarifaCEDTO());
            }
            return lstR;
        }

        public TarifaCEDTO ObtTarifaCE(int Id)
        {
            var objR = _tarifaceLogic.ObtTarifaCE(Id);
            return objR.GetTarifaCEDTO();
        }

        public RespuestaDTO EditTarifaCE(TarifaCEDTO obj)
        {
            var objR = _tarifaceLogic.EditTarifaCE(obj.SetTarifaCE());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimTarifaCE(int Id)
        {
            var objR = _tarifaceLogic.ElimTarifaCE(Id);
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO AprobarTarifaCE(TarifaCEDTO obj)
        {
            var objR = _tarifaceLogic.AprobarTarifaCE(obj.SetTarifaCE());
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region TarifaCEDoc
        public DocumentoDTO ObtTarifaCEDocNombre(int Id)
        {
            return _tarifacedocLogic.ObtTarifaCEDocNombre(Id).GetDocumentoDTO();
        }
        public RespuestaDTO EditTarifaCEDoc(TarifaCEDocDTO obj)
        {
            var objR = _tarifacedocLogic.EditTarifaCEDoc(obj.SetTarifaCEDoc());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimTarifaCEDoc(int Id)
        {
            var objR = _tarifacedocLogic.ElimTarifaCEDoc(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Bitacora
        public List<BitacoraDTO> ObtBitacora(int Id, string Tabla)
        {
            var list = _bitacoraLogic.ObtBitacora(Id, Tabla);
            var lstR = new List<BitacoraDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetBitacoraDTO());
            }
            return lstR;
        }

        public RespuestaDTO EditBitacora(BitacoraDTO obj)
        {
            var objR = _bitacoraLogic.EditBitacora(obj.SetBitacora());
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region CondEspeCli
        public List<CondEspeCliDTO> ObtCondEspeCli()
        {
            var list = _condespecliLogic.ObtCondEspeCli();
            var lstR = new List<CondEspeCliDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetCondEspeCliDTO());
            }
            return lstR;
        }

        public List<CondEspeCliDTO> ObtCondEspeCliHistorico()
        {
            var list = _condespecliLogic.ObtCondEspeCliHistorico();
            var lstR = new List<CondEspeCliDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetCondEspeCliDTO());
            }
            return lstR;
        }

        public CondEspeCliDTO ObtCondEspeCli(int Id)
        {
            var objR = _condespecliLogic.ObtCondEspeCli(Id);
            return objR.GetCondEspeCliDTO();
        }

        public RespuestaDTO EditCondEspeCli(CondEspeCliDTO obj)
        {
            var objR = _condespecliLogic.EditCondEspeCli(obj.SetCondEspeCli());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimCondEspeCli(int Id)
        {
            var objR = _condespecliLogic.ElimCondEspeCli(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region CondEspeCliDoc
        public DocumentoDTO ObtCondEspeCliDocNombre(int Id)
        {
            return _condespeclidocLogic.ObtCondEspeCliDocNombre(Id).GetDocumentoDTO();
        }
        public RespuestaDTO EditCondEspeCliDoc(CondEspeCliDocDTO obj)
        {
            var objR = _condespeclidocLogic.EditCondEspeCliDoc(obj.SetCondEspeCliDoc());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimCondEspeCliDoc(int Id)
        {
            var objR = _condespeclidocLogic.ElimCondEspeCliDoc(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region CondEspeCliDetalle
        public CondEspeCliDetalleDTO ObtCondEspeCliDetalle(int Id)
        {
            var objR = _condespeclidetalleLogic.ObtCondEspeCliDetalle(Id);
            return objR.GetCondEspeCliDetalleDTO();
        }
        public RespuestaDTO EditCondEspeCliDetalle(CondEspeCliDetalleDTO obj)
        {
            var objR = _condespeclidetalleLogic.EditCondEspeCliDetalle(obj.SetCondEspeCliDetalle());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimCondEspeCliDetalle(int Id)
        {
            var objR = _condespeclidetalleLogic.ElimCondEspeCliDetalle(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region CondEspeCliDia
        public List<CondEspeCliDiaDTO> ObtCondEspeCliDia(int Id)
        {
            var list = _condespeclidiaLogic.ObtCondEspeCliDia(Id);
            var lstR = new List<CondEspeCliDiaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetCondEspeCliDiaDTO());
            }
            return lstR;
        }
        public RespuestaDTO EditCondEspeCliDia(CondEspeCliDiaDTO obj)
        {
            var objR = _condespeclidiaLogic.EditCondEspeCliDia(obj.SetCondEspeCliDia());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimCondEspeCliDia(int Id)
        {
            var objR = _condespeclidiaLogic.ElimCondEspeCliDia(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region CargaLiquiC
        public List<CargaLiquiCDTO> ObtEnviadosC()
        {
            var list = _cargaliquiCLogic.ObtEnviadosC();
            var lstR = new List<CargaLiquiCDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetCargaLiquiCDTO());
            }
            return lstR;
        }

        public List<CargaLiquiCDTO> ObtCargaLiquiC()
        {
            var list = _cargaliquiCLogic.ObtCargaLiquiC();
            var lstR = new List<CargaLiquiCDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetCargaLiquiCDTO());
            }
            return lstR;
        }

        public CargaLiquiCDTO ObtCargaLiquiC(int Id)
        {
            var objR = _cargaliquiCLogic.ObtCargaLiquiC(Id);
            return objR.GetCargaLiquiCDTO();
        }

        public void EditCargaLiquiC(CargaLiquiCDTO obj)
        {
            _cargaliquiCLogic.EditCargaLiquiC(obj.SetCargaLiquiC());
        }

        public RespuestaDTO ElimCargaLiquiC(int Id)
        {
            var objR = _cargaliquiCLogic.ElimCargaLiquiC(Id);
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO EnviarCargaLiquiC(string Id, string Correo)
        {
            var objR = _cargaliquiCLogic.EnviarCargaLiquiC(Id, Correo, "Enviado", "");
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO AprobarCargaLiquiC(string Id, string Correo, string Comentario)
        {
            var objR = _cargaliquiCLogic.EnviarCargaLiquiC(Id, Correo, "Aprobado", Comentario);
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO RechazarCargaLiquiC(string Id, string Correo, string Comentario)
        {
            var objR = _cargaliquiCLogic.EnviarCargaLiquiC(Id, Correo, "Rechazado", Comentario);
            return objR.GetRespuestaDTO();
        }

        public DocumentoDTO ObtCargaLiquiCDocNombre(int Id)
        {
            return _cargaliquiCLogic.ObtCargaLiquiCDocNombre(Id).GetDocumentoDTO();
        }
        #endregion

        #region CargaLiqui
        public CargaLiquiDTO ObtCargaLiqui(int Id)
        {
            var objR = _cargaliquiLogic.ObtCargaLiqui(Id);
            return objR.GetCargaLiquiDTO();
        }
        #endregion

        #region Sucursal
        public List<SucursalDTO> ObtSucursal()
        {
            var list = _sucursalLogic.ObtSucursal();
            var lstR = new List<SucursalDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetSucursalDTO());
            }
            return lstR;
        }

        public SucursalDTO ObtSucursal(int Id)
        {
            var objR = _sucursalLogic.ObtSucursal(Id);
            return objR.GetSucursalDTO();
        }

        public RespuestaDTO EditSucursal(SucursalDTO obj)
        {
            var objR = _sucursalLogic.EditSucursal(obj.SetSucursal());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimSucursal(int Id)
        {
            var objR = _sucursalLogic.ElimSucursal(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Producto
        public List<ProductoDTO> ObtProducto()
        {
            var list = _productoLogic.ObtProducto();
            var lstR = new List<ProductoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetProductoDTO());
            }
            return lstR;
        }

        public List<ProductoDTO> ObtAllProductoxProveedor(string desc, int id)
        {
            var list = _productoLogic.ObtAllProductoxProveedor(desc, id);
            var lstR = new List<ProductoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetProductoDTO());
            }
            return lstR;
        }

        public List<ProductoDTO> ObtAllProducto(string desc)
        {
            var list = _productoLogic.ObtAllProducto(desc);
            var lstR = new List<ProductoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetProductoDTO());
            }
            return lstR;
        }

        public ProductoDTO ObtProducto(int Id)
        {
            var objR = _productoLogic.ObtProducto(Id);
            return objR.GetProductoDTO();
        }

        public RespuestaDTO EditProducto(ProductoDTO obj)
        {
            var objR = _productoLogic.EditProducto(obj.SetProducto());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimProducto(int Id)
        {
            var objR = _productoLogic.ElimProducto(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion
        
        #region Tarifario
        public List<TarifarioDTO> ObtTarifario()
        {
            var list = _tarifarioLogic.ObtTarifario();
            var lstR = new List<TarifarioDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetTarifarioDTO());
            }
            return lstR;
        }

        public TarifarioDTO ObtTarifario(int Id)
        {
            var objR = _tarifarioLogic.ObtTarifario(Id);
            return objR.GetTarifarioDTO();
        }

        public RespuestaDTO EditTarifario(TarifarioDTO obj)
        {
            var objR = _tarifarioLogic.EditTarifario(obj.SetTarifario());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimTarifario(int Id)
        {
            var objR = _tarifarioLogic.ElimTarifario(Id);
            return objR.GetRespuestaDTO();
        }

        //public List<EstadoDTO> ObtEstado()
        //{
        //    var list = _EstadoLogic.ObtEstado();
        //    var lstR = new List<EstadoDTO>();
        //    foreach (var item in list)
        //    {
        //        lstR.Add(item.GetEstadoDTO());
        //    }
        //    return lstR;
        //}

        #endregion

        #region TarifarioDoc
        public DocumentoDTO ObtTarifarioDocNombre(int Id)
        {
            return _tarifarioDocLogic.ObtTarifarioDocNombre(Id).GetDocumentoDTO();
        }
        public RespuestaDTO EditTarifarioDoc(TarifarioDocDTO obj)
        {
            var objR = _tarifarioDocLogic.EditTarifarioDoc(obj.SetTarifarioDoc());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimTarifarioDoc(int Id)
        {
            var objR = _tarifarioDocLogic.ElimTarifarioDoc(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Proveedor
        public List<ProveedorDTO> ObtProveedor()
        {
            var list = _proveedorLogic.ObtProveedor();
            var lstR = new List<ProveedorDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetProveedorDTO());
            }
            return lstR;
        }

        public List<ProveedorDTO> ObtAllProveedor(string desc)
        {
            var list = _proveedorLogic.ObtAllProveedor(desc);
            var lstR = new List<ProveedorDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetProveedorDTO());
            }
            return lstR;
        }

        public List<ProveedorDTO> ObtAllProveedorTarifaProducto(string desc, int id)
        {
            var list = _proveedorLogic.ObtAllProveedorTarifaProducto(desc, id);
            var lstR = new List<ProveedorDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetProveedorDTO());
            }
            return lstR;
        }

        public ProveedorDTO ObtProveedor(int Id)
        {
            var objR = _proveedorLogic.ObtProveedor(Id);
            return objR.GetProveedorDTO();
        }

        public RespuestaDTO EditProveedor(ProveedorDTO obj)
        {
            var objR = _proveedorLogic.EditProveedor(obj.SetProveedor());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimProveedor(int Id)
        {
            var objR = _proveedorLogic.ElimProveedor(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region DatoComercial
        public DatoComercialDTO ObtDatoComercial(int Id)
        {
            var objR = _datocomercialLogic.ObtDatoComercial(Id);
            return objR.GetDatoComercialDTO();
        }
        public RespuestaDTO EditDatoComercial(DatoComercialDTO obj)
        {
            var objR = _datocomercialLogic.EditDatoComercial(obj.SetDatoComercial());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimDatoComercial(int Id)
        {
            var objR = _datocomercialLogic.ElimDatoComercial(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region ContactoProveedor
        public ContactoProveedorDTO ObtContactoProveedor(int Id)
        {
            var objR = _contactoproveedorLogic.ObtContactoProveedor(Id);
            return objR.GetContactoProveedorDTO();
        }
        public RespuestaDTO EditContactoProveedor(ContactoProveedorDTO obj)
        {
            var objR = _contactoproveedorLogic.EditContactoProveedor(obj.SetContactoProveedor());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimContactoProveedor(int Id)
        {
            var objR = _contactoproveedorLogic.ElimContactoProveedor(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region AreaSolicitante
        public List<AreaSolicitanteDTO> ObtAreaSolicitante()
        {
            var list = _areasolLogic.ObtAreaSolicitante();
            var lstR = new List<AreaSolicitanteDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetAreaSolicitanteDTO());
            }
            return lstR;
        }

        //public List<AreaSolicitanteDTO> ObtAreaSolicitantexEmpresa(int Id)
        //{
        //    var list = _areasolLogic.ObtAreaSolicitantexEmpresa(Id);
        //    var lstR = new List<AreaSolicitanteDTO>();
        //    foreach (var item in list)
        //    {
        //        lstR.Add(item.GetAreaSolicitanteDTO());
        //    }
        //    return lstR;
        //}

        public AreaSolicitanteDTO ObtAreaSolicitante(int Id)
        {
            var objR = _areasolLogic.ObtAreaSolicitante(Id);
            return objR.GetAreaSolicitanteDTO();
        }

        public RespuestaDTO EditAreaSolicitante(AreaSolicitanteDTO obj)
        {
            var objR = _areasolLogic.EditAreaSolicitante(obj.SetAreaSolicitante());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimAreaSolicitante(int Id)
        {
            var objR = _areasolLogic.ElimAreaSolicitante(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region CentroCosto
        public List<CentroCostoDTO> ObtCentroCosto()
        {
            var list = _centrocostoLogic.ObtCentroCosto();
            var lstR = new List<CentroCostoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetCentroCostoDTO());
            }
            return lstR;
        }

        public CentroCostoDTO ObtCentroCosto(int Id)
        {
            var objR = _centrocostoLogic.ObtCentroCosto(Id);
            return objR.GetCentroCostoDTO();
        }

        public RespuestaDTO EditCentroCosto(CentroCostoDTO obj)
        {
            var objR = _centrocostoLogic.EditCentroCosto(obj.SetCentroCosto());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimCentroCosto(int Id)
        {
            var objR = _centrocostoLogic.ElimCentroCosto(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Familia
        public List<FamiliaDTO> ObtFamilia()
        {
            var list = _FamiliaLogic.ObtFamilia();
            var lstR = new List<FamiliaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetFamiliaDTO());
            }
            return lstR;
        }

        public FamiliaDTO ObtFamilia(int Id)
        {
            var objR = _FamiliaLogic.ObtFamilia(Id);
            return objR.GetFamiliaDTO();
        }

        public RespuestaDTO EditFamilia(FamiliaDTO obj)
        {
            var objR = _FamiliaLogic.EditFamilia(obj.SetFamilia());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimFamilia(int Id)
        {
            var objR = _FamiliaLogic.ElimFamilia(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region SubFamilia
        public List<SubFamiliaDTO> ObtSubFamilia()
        {
            var list = _SubFamiliaLogic.ObtSubFamilia();
            var lstR = new List<SubFamiliaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetSubFamiliaDTO());
            }
            return lstR;
        }

        public SubFamiliaDTO ObtSubFamilia(int Id)
        {
            var objR = _SubFamiliaLogic.ObtSubFamilia(Id);
            return objR.GetSubFamiliaDTO();
        }

        public RespuestaDTO EditSubFamilia(SubFamiliaDTO obj)
        {
            var objR = _SubFamiliaLogic.EditSubFamilia(obj.SetSubFamilia());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimSubFamilia(int Id)
        {
            var objR = _SubFamiliaLogic.ElimSubFamilia(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Pedido
        public List<PedidoDTO> ObtAllPedido(string desc)
        {
            var list = _pedidoLogic.ObtAllPedido(desc);
            var lstR = new List<PedidoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetPedidoDTO());
            }
            return lstR;
        }
        public RespuestaDTO GenerarCotizacion(int Id, int IdPadre)
        {
            var objR = _pedidoLogic.GenerarCotizacion(Id, IdPadre);
            return objR.GetRespuestaDTO();
        }
        public List<TarifarioDTO> ObtPedidoMapProductos(int Id)
        {
            var list = _pedidoLogic.ObtPedidoMapProductos(Id);
            var lstR = new List<TarifarioDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetTarifarioDTO());
            }
            return lstR;
        }
        public List<PedidoDTO> ObtPedidoPorCotizar()
        {
            var list = _pedidoLogic.ObtPedidoPorCotizar();
            var lstR = new List<PedidoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetPedidoDTO());
            }
            return lstR;
        }
        public List<PedidoDTO> ObtPedidoPorValidar()
        {
            var list = _pedidoLogic.ObtPedidoPorValidar();
            var lstR = new List<PedidoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetPedidoDTO());
            }
            return lstR;
        }
        public List<PedidoDTO> ObtPedido()
        {
            var list = _pedidoLogic.ObtPedido();
            var lstR = new List<PedidoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetPedidoDTO());
            }
            return lstR;
        }
        public PedidoDTO ObtPedido(int Id)
        {
            var objR = _pedidoLogic.ObtPedido(Id);
            return objR.GetPedidoDTO();
        }
        public RespuestaDTO EditPedido(PedidoDTO obj)
        {
            var objR = _pedidoLogic.EditPedido(obj.SetPedido());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimPedido(int Id)
        {
            var objR = _pedidoLogic.ElimPedido(Id);
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO AprobarPedido(string Id, string Correo, string Comentario, string Usuario)
        {
            var objR = _pedidoLogic.ValidarPedido(Id, Correo, "Aprobado", Comentario, Usuario);
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO RechazarPedido(string Id, string Correo, string Comentario, string Usuario)
        {
            var objR = _pedidoLogic.ValidarPedido(Id, Correo, "Rechazado", Comentario, Usuario);
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ObservarPedido(string Id, string Correo, string Comentario, string Usuario)
        {
            var objR = _pedidoLogic.ValidarPedido(Id, Correo, "Observado", Comentario, Usuario);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region DetallePedido

        public DetallePedidoDTO ObtDetallePedido(int Id)
        {
            var objR = _detallepedidoLogic.ObtDetallePedido(Id);
            return objR.GetDetallePedidoDTO();
        }

        public RespuestaDTO EditDetallePedido(DetallePedidoDTO obj)
        {
            var objR = _detallepedidoLogic.EditDetallePedido(obj.SetDetallePedido());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimDetallePedido(int Id)
        {
            var objR = _detallepedidoLogic.ElimDetallePedido(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion


        #region Cotizacion
        public List<CotizacionDTO> ObtAllCotizacion(string desc)
        {
            var list = _cotizacionLogic.ObtAllCotizacion(desc);
            var lstR = new List<CotizacionDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetCotizacionDTO());
            }
            return lstR;
        }
        public RespuestaDTO GenerarOC(int Id, string usuario)
        {
            var objR = _cotizacionLogic.GenerarOC(Id, usuario);
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO EnviarCotizacion(int Id)
        {
            var objR = _cotizacionLogic.EnviarCotizacion(Id);
            return objR.GetRespuestaDTO();
        }
        public List<DetalleCotizacionDTO> AjustarCotizacion(int IdProv, int IdCotizacion)
        {
            var list = _cotizacionLogic.AjustarCotizacion(IdProv, IdCotizacion);
            var lstR = new List<DetalleCotizacionDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetDetalleCotizacionDTO());
            }
            return lstR;
        }
        public List<CotizacionDTO> ObtCotizacion()
        {
            var list = _cotizacionLogic.ObtCotizacion();
            var lstR = new List<CotizacionDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetCotizacionDTO());
            }
            return lstR;
        }
        public CotizacionDTO ObtCotizacion(int Id)
        {
            var objR = _cotizacionLogic.ObtCotizacion(Id);
            return objR.GetCotizacionDTO();
        }
        public RespuestaDTO EditCotizacion(CotizacionDTO obj)
        {
            var objR = _cotizacionLogic.EditCotizacion(obj.SetCotizacion());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimCotizacion(int Id)
        {
            var objR = _cotizacionLogic.ElimCotizacion(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region DetalleCotizacion

        public DetalleCotizacionDTO ObtDetalleCotizacion(int Id)
        {
            var objR = _detallecotizacionLogic.ObtDetalleCotizacion(Id);
            return objR.GetDetalleCotizacionDTO();
        }

        public RespuestaDTO EditDetalleCotizacion(DetalleCotizacionDTO obj)
        {
            var objR = _detallecotizacionLogic.EditDetalleCotizacion(obj.SetDetalleCotizacion());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimDetalleCotizacion(int Id)
        {
            var objR = _detallecotizacionLogic.ElimDetalleCotizacion(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region OrdenCompra

        public List<OrdenCompraDTO> ObtAllOrdenCompra(string desc)
        {
            var list = _ordencompraLogic.ObtAllOrdenCompra(desc);
            var lstR = new List<OrdenCompraDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetOrdenCompraDTO());
            }
            return lstR;
        }
        public RespuestaDTO FactOrdenCompra(int Id, string Factura)
        {
            var objR = _ordencompraLogic.FactOrdenCompra(Id, Factura);
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO EnviarOrdenCompra(int Id)
        {
            var objR = _ordencompraLogic.EnviarOrdenCompra(Id);
            return objR.GetRespuestaDTO();
        }
        public List<OrdenCompraDTO> ObtOrdenCompraPorValidar()
        {
            var list = _ordencompraLogic.ObtOrdenCompraPorValidar();
            var lstR = new List<OrdenCompraDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetOrdenCompraDTO());
            }
            return lstR;
        }
        public List<OrdenCompraDTO> ObtOrdenCompra()
        {
            var list = _ordencompraLogic.ObtOrdenCompra();
            var lstR = new List<OrdenCompraDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetOrdenCompraDTO());
            }
            return lstR;
        }
        public OrdenCompraDTO ObtOrdenCompra(int Id)
        {
            var objR = _ordencompraLogic.ObtOrdenCompra(Id);
            return objR.GetOrdenCompraDTO();
        }
        public RespuestaDTO EditOrdenCompra(OrdenCompraDTO obj)
        {
            var objR = _ordencompraLogic.EditOrdenCompra(obj.SetOrdenCompra());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimOrdenCompra(int Id)
        {
            var objR = _ordencompraLogic.ElimOrdenCompra(Id);
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO AprobarOrdenCompra(string Id, string Correo, string Comentario, string Usuario)
        {
            var objR = _ordencompraLogic.ValidarOrdenCompra(Id, Correo, "Aprobado", Comentario, Usuario);
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO RechazarOrdenCompra(string Id, string Correo, string Comentario, string Usuario)
        {
            var objR = _ordencompraLogic.ValidarOrdenCompra(Id, Correo, "Rechazado", Comentario, Usuario);
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ObservarOrdenCompra(string Id, string Correo, string Comentario, string Usuario)
        {
            var objR = _ordencompraLogic.ValidarOrdenCompra(Id, Correo, "Observado", Comentario, Usuario);
            return objR.GetRespuestaDTO();
        }
        public List<IMP_ORDENCOMPRA> ImprimirOrdenCompra(int Id)
        {
            return _ordencompraLogic.ImprimirOrdenCompra(Id);
        }

        #endregion

        #region DetalleOrdenCompra

        public DetalleOrdenCompraDTO ObtDetalleOrdenCompra(int Id)
        {
            var objR = _detalleordencompraLogic.ObtDetalleOrdenCompra(Id);
            return objR.GetDetalleOrdenCompraDTO();
        }

        public RespuestaDTO EditDetalleOrdenCompra(DetalleOrdenCompraDTO obj)
        {
            var objR = _detalleordencompraLogic.EditDetalleOrdenCompra(obj.SetDetalleOrdenCompra());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimDetalleOrdenCompra(int Id)
        {
            var objR = _detalleordencompraLogic.ElimDetalleOrdenCompra(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region OrdenCompraDoc
        public DocumentoDTO ObtOrdenCompraDocNombre(int Id)
        {
            return _ordencompradocLogic.ObtOrdenCompraDocNombre(Id).GetDocumentoDTO();
        }
        public RespuestaDTO EditOrdenCompraDoc(OrdenCompraDocDTO obj)
        {
            var objR = _ordencompradocLogic.EditOrdenCompraDoc(obj.SetOrdenCompraDoc());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimOrdenCompraDoc(int Id)
        {
            var objR = _ordencompradocLogic.ElimOrdenCompraDoc(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Ubigeo
        public List<TablaDTO> ObtPais()
        {
            var list = _ubigeoLogic.ObtPais();
            var lstR = new List<TablaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetTablaDTO());
            }
            return lstR;
        }

        public List<UbigeoDTO> ObtDepartamento(int IdPais)
        {
            var list = _ubigeoLogic.ObtDepartamento(IdPais);
            var lstR = new List<UbigeoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetUbigeoDTO());
            }
            return lstR;
        }

        public List<UbigeoDTO> ObtProvincia(int IdDep)
        {
            var list = _ubigeoLogic.ObtProvincia(IdDep);
            var lstR = new List<UbigeoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetUbigeoDTO());
            }
            return lstR;
        }

        public List<UbigeoDTO> ObtDistrito(int IdProv)
        {
            var list = _ubigeoLogic.ObtDistrito(IdProv);
            var lstR = new List<UbigeoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetUbigeoDTO());
            }
            return lstR;
        }
        #endregion

        #region Impuesto
        public List<ImpuestoDTO> ObtImpuesto()
        {
            var list = _impuestoLogic.ObtImpuesto();
            var lstR = new List<ImpuestoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetImpuestoDTO());
            }
            return lstR;
        }

        public ImpuestoDTO ObtImpuesto(int Id)
        {
            var objR = _impuestoLogic.ObtImpuesto(Id);
            return objR.GetImpuestoDTO();
        }

        public RespuestaDTO EditImpuesto(ImpuestoDTO obj)
        {
            var objR = _impuestoLogic.EditImpuesto(obj.SetImpuesto());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimImpuesto(int Id)
        {
            var objR = _impuestoLogic.ElimImpuesto(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region ImpuestoProveedor
        public ImpuestoProveedorDTO ObtImpuestoProveedor(int Id)
        {
            var objR = _impuestoproveedorLogic.ObtImpuestoProveedor(Id);
            return objR.GetImpuestoProveedorDTO();
        }
        public RespuestaDTO EditImpuestoProveedor(ImpuestoProveedorDTO obj)
        {
            var objR = _impuestoproveedorLogic.EditImpuestoProveedor(obj.SetImpuestoProveedor());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimImpuestoProveedor(int Id)
        {
            var objR = _impuestoproveedorLogic.ElimImpuestoProveedor(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Banco
        public List<BancoDTO> ObtBanco()
        {
            var list = _bancoLogic.ObtBanco();
            var lstR = new List<BancoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetBancoDTO());
            }
            return lstR;
        }

        public BancoDTO ObtBanco(int Id)
        {
            var objR = _bancoLogic.ObtBanco(Id);
            return objR.GetBancoDTO();
        }

        public RespuestaDTO EditBanco(BancoDTO obj)
        {
            var objR = _bancoLogic.EditBanco(obj.SetBanco());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimBanco(int Id)
        {
            var objR = _bancoLogic.ElimBanco(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Tarea
        public List<TareaDTO> ObtTarea()
        {
            var list = _tareaLogic.ObtTarea();
            var lstR = new List<TareaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetTareaDTO());
            }
            return lstR;
        }

        public TareaDTO ObtTarea(int Id)
        {
            var objR = _tareaLogic.ObtTarea(Id);
            return objR.GetTareaDTO();
        }

        public RespuestaDTO EditTarea(TareaDTO obj)
        {
            var objR = _tareaLogic.EditTarea(obj.SetTarea());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimTarea(int Id)
        {
            var objR = _tareaLogic.ElimTarea(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Empresa
        public List<EmpresaDTO> ObtEmpresa()
        {
            var list = _empresaLogic.ObtEmpresa();
            var lstR = new List<EmpresaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetEmpresaDTO());
            }
            return lstR;
        }

        public EmpresaDTO ObtEmpresa(int Id)
        {
            var objR = _empresaLogic.ObtEmpresa(Id);
            return objR.GetEmpresaDTO();
        }

        public RespuestaDTO EditEmpresa(EmpresaDTO obj)
        {
            var objR = _empresaLogic.EditEmpresa(obj.SetEmpresa());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimEmpresa(int Id)
        {
            var objR = _empresaLogic.ElimEmpresa(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Provision
        public List<ProvisionDTO> ObtProvision()
        {
            var list = _provisionLogic.ObtProvision();
            var lstR = new List<ProvisionDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetProvisionDTO());
            }
            return lstR;
        }

        public ProvisionDTO ObtProvision(int Id)
        {
            var objR = _provisionLogic.ObtProvision(Id);
            return objR.GetProvisionDTO();
        }

        public RespuestaDTO EditProvision(ProvisionDTO obj)
        {
            var objR = _provisionLogic.EditProvision(obj.SetProvision());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimProvision(int Id)
        {
            var objR = _provisionLogic.ElimProvision(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

    }
}