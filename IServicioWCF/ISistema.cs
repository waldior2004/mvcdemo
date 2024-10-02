using com.msc.infraestructure.entities.impresion;
using com.msc.services.dto;
using System.Collections.Generic;
using System.ServiceModel;

namespace com.msc.services.interfaces
{
    [ServiceContract]
    public partial interface ISistema
    {

        #region Documento
        [OperationContract]
        RespuestaDTO EditDocumento(DocumentoDTO obj);

        [OperationContract]
        RespuestaDTO ElimDocumento(int Id);
        #endregion

        #region Nave
        [OperationContract]
        List<NaveDTO> ObtAllNave(string desc);
        #endregion

        #region Puerto
        [OperationContract]
        List<PuertoDTO> ObtPuerto();
        #endregion

        #region Viaje
        [OperationContract]
        List<ViajeDTO> ObtAllViaje(string desc);
        [OperationContract]
        List<ViajeDTO> ObtViajexNave(string id, int port);
        #endregion

        #region Cliente
        [OperationContract]
        List<ClienteDTO> ObtCliente();
        [OperationContract]
        List<ClienteDTO> ObtAllCliente(string desc);
        #endregion

        #region GrupoTabla
        [OperationContract]
        List<GrupoTablaDTO> ObtGrupoTabla();

        [OperationContract(Name = "ObtGrupoTablaxId")]
        GrupoTablaDTO ObtGrupoTabla(int Id);

        [OperationContract]
        RespuestaDTO EditGrupoTabla(GrupoTablaDTO obj);

        [OperationContract]
        RespuestaDTO ElimGrupoTabla(int Id);
        #endregion

        #region Tabla
        [OperationContract]
        List<TablaDTO> ObtTabla();

        [OperationContract(Name = "ObtTablaxId")]
        TablaDTO ObtTabla(int Id);

        [OperationContract]
        List<TablaDTO> ObtTablaGrupo(string Codigo);

        [OperationContract]
        RespuestaDTO EditTabla(TablaDTO obj);

        [OperationContract]
        RespuestaDTO ElimTabla(int Id);
        #endregion

        #region Externo
        [OperationContract]
        List<ExternoDTO> ObtExterno();

        [OperationContract(Name = "ObtExternoxId")]
        ExternoDTO ObtExterno(int Id);

        [OperationContract]
        RespuestaDTO ResetKeyExterno(int Id);

        [OperationContract]
        RespuestaDTO EditExterno(ExternoDTO obj);

        [OperationContract]
        RespuestaDTO ElimExterno(int Id);
        #endregion

        #region TarifaCE
        [OperationContract]
        List<TarifaCEDTO> ObtTarifaCE();

        [OperationContract]
        List<TarifaCEDTO> ObtTarifaHistoricoCE();

        [OperationContract(Name = "ObtTarifaCExId")]
        TarifaCEDTO ObtTarifaCE(int Id);

        [OperationContract]
        RespuestaDTO EditTarifaCE(TarifaCEDTO obj);

        [OperationContract]
        RespuestaDTO ElimTarifaCE(int Id);

        [OperationContract]
        RespuestaDTO AprobarTarifaCE(TarifaCEDTO obj);
        #endregion

        #region TarifaCEDoc
        [OperationContract]
        DocumentoDTO ObtTarifaCEDocNombre(int Id);
        [OperationContract]
        RespuestaDTO EditTarifaCEDoc(TarifaCEDocDTO obj);
        [OperationContract]
        RespuestaDTO ElimTarifaCEDoc(int Id);
        #endregion

        #region Bitacora
        [OperationContract]
        List<BitacoraDTO> ObtBitacora(int Id, string Tabla);
        [OperationContract]
        RespuestaDTO EditBitacora(BitacoraDTO obj);
        #endregion

        #region CondEspeCli
        [OperationContract]
        List<CondEspeCliDTO> ObtCondEspeCli();

        [OperationContract]
        List<CondEspeCliDTO> ObtCondEspeCliHistorico();

        [OperationContract(Name = "ObtCondEspeClixId")]
        CondEspeCliDTO ObtCondEspeCli(int Id);

        [OperationContract]
        RespuestaDTO EditCondEspeCli(CondEspeCliDTO obj);

        [OperationContract]
        RespuestaDTO ElimCondEspeCli(int Id);
        #endregion

        #region CondEspeCliDoc
        [OperationContract]
        DocumentoDTO ObtCondEspeCliDocNombre(int Id);
        [OperationContract]
        RespuestaDTO EditCondEspeCliDoc(CondEspeCliDocDTO obj);
        [OperationContract]
        RespuestaDTO ElimCondEspeCliDoc(int Id);
        #endregion

        #region CondEspeCliDetalle
        [OperationContract]
        CondEspeCliDetalleDTO ObtCondEspeCliDetalle(int Id);
        [OperationContract]
        RespuestaDTO EditCondEspeCliDetalle(CondEspeCliDetalleDTO obj);
        [OperationContract]
        RespuestaDTO ElimCondEspeCliDetalle(int Id);
        #endregion

        #region CondEspeCliDia
        [OperationContract]
        List<CondEspeCliDiaDTO> ObtCondEspeCliDia(int Id);
        [OperationContract]
        RespuestaDTO EditCondEspeCliDia(CondEspeCliDiaDTO obj);
        [OperationContract]
        RespuestaDTO ElimCondEspeCliDia(int Id);
        #endregion

        #region CargaLiquiC
        [OperationContract]
        List<CargaLiquiCDTO> ObtEnviadosC();

        [OperationContract]
        List<CargaLiquiCDTO> ObtCargaLiquiC();

        [OperationContract(Name = "ObtCargaLiquiCxId")]
        CargaLiquiCDTO ObtCargaLiquiC(int Id);

        [OperationContract(IsOneWay = true)]
        void EditCargaLiquiC(CargaLiquiCDTO obj);

        [OperationContract]
        RespuestaDTO EnviarCargaLiquiC(string Id, string Correo);

        [OperationContract]
        RespuestaDTO AprobarCargaLiquiC(string Id, string Correo, string Comentario);

        [OperationContract]
        RespuestaDTO RechazarCargaLiquiC(string Id, string Correo, string Comentario);

        [OperationContract]
        RespuestaDTO ElimCargaLiquiC(int Id);

        [OperationContract]
        DocumentoDTO ObtCargaLiquiCDocNombre(int Id);
        #endregion

        #region CargaLiqui
        [OperationContract]
        CargaLiquiDTO ObtCargaLiqui(int Id);
        #endregion

        #region Producto
        [OperationContract]
        List<ProductoDTO> ObtAllProductoxProveedor(string desc, int id);
        [OperationContract]
        List<ProductoDTO> ObtProducto();

        [OperationContract(Name = "ObtProductoxId")]
        ProductoDTO ObtProducto(int Id);

        [OperationContract]
        List<ProductoDTO> ObtAllProducto(string desc);

        [OperationContract]
        RespuestaDTO EditProducto(ProductoDTO obj);

        [OperationContract]
        RespuestaDTO ElimProducto(int Id);
        #endregion

        #region Tarifario
        [OperationContract]
        List<TarifarioDTO> ObtTarifario();

        //[OperationContract]
        //List<EstadoDTO> ObtEstado();

        //[OperationContract]
        //List<MonedaDTO> ObtMoneda();

        [OperationContract(Name = "ObtTarifarioxId")]
        TarifarioDTO ObtTarifario(int Id);

        [OperationContract]
        RespuestaDTO EditTarifario(TarifarioDTO obj);

        [OperationContract]
        RespuestaDTO ElimTarifario(int Id);
        #endregion

        #region TarifarioDoc
        [OperationContract]
        DocumentoDTO ObtTarifarioDocNombre(int Id);
        [OperationContract]
        RespuestaDTO EditTarifarioDoc(TarifarioDocDTO obj);
        [OperationContract]
        RespuestaDTO ElimTarifarioDoc(int Id);
        #endregion

        #region Proveedor
        [OperationContract]
        List<ProveedorDTO> ObtAllProveedorTarifaProducto(string desc, int id);
        [OperationContract]
        List<ProveedorDTO> ObtProveedor();

        [OperationContract]
        List<ProveedorDTO> ObtAllProveedor(string desc);

        [OperationContract(Name = "ObtProveedorxId")]
        ProveedorDTO ObtProveedor(int Id);

        [OperationContract]
        RespuestaDTO EditProveedor(ProveedorDTO obj);

        [OperationContract]
        RespuestaDTO ElimProveedor(int Id);
        #endregion

        #region DatoComercial
        [OperationContract]
        DatoComercialDTO ObtDatoComercial(int Id);
        [OperationContract]
        RespuestaDTO EditDatoComercial(DatoComercialDTO obj);
        [OperationContract]
        RespuestaDTO ElimDatoComercial(int Id);
        #endregion

        #region ContactoProveedor
        [OperationContract]
        ContactoProveedorDTO ObtContactoProveedor(int Id);
        [OperationContract]
        RespuestaDTO EditContactoProveedor(ContactoProveedorDTO obj);
        [OperationContract]
        RespuestaDTO ElimContactoProveedor(int Id);
        #endregion

        #region Sucursal
        [OperationContract]
        List<SucursalDTO> ObtSucursal();
        [OperationContract(Name = "ObtSucursalxId")]
        SucursalDTO ObtSucursal(int Id);
        [OperationContract]
        RespuestaDTO EditSucursal(SucursalDTO obj);
        [OperationContract]
        RespuestaDTO ElimSucursal(int Id);
        #endregion

        #region CentroCosto
        [OperationContract]
        List<CentroCostoDTO> ObtCentroCosto();
        [OperationContract(Name = "ObtCentroCostoxId")]
        CentroCostoDTO ObtCentroCosto(int Id);
        [OperationContract]
        RespuestaDTO EditCentroCosto(CentroCostoDTO obj);
        [OperationContract]
        RespuestaDTO ElimCentroCosto(int Id);
        #endregion

        #region AreaSolicitante
        [OperationContract]
        List<AreaSolicitanteDTO> ObtAreaSolicitante();
        [OperationContract(Name = "ObtAreaSolicitantexId")]
        AreaSolicitanteDTO ObtAreaSolicitante(int Id);
        [OperationContract]
        RespuestaDTO EditAreaSolicitante(AreaSolicitanteDTO obj);
        [OperationContract]
        RespuestaDTO ElimAreaSolicitante(int Id);
        #endregion

        #region Familia
        [OperationContract]
        List<FamiliaDTO> ObtFamilia();

        [OperationContract(Name = "ObtFamiliaxId")]
        FamiliaDTO ObtFamilia(int Id);

        [OperationContract]
        RespuestaDTO EditFamilia(FamiliaDTO obj);

        [OperationContract]
        RespuestaDTO ElimFamilia(int Id);
        #endregion

        #region SubFamilia
        [OperationContract]
        List<SubFamiliaDTO> ObtSubFamilia();

        [OperationContract(Name = "ObtSubFamiliaxId")]
        SubFamiliaDTO ObtSubFamilia(int Id);

        [OperationContract]
        RespuestaDTO EditSubFamilia(SubFamiliaDTO obj);

        [OperationContract]
        RespuestaDTO ElimSubFamilia(int Id);
        #endregion

        #region Pedido
        [OperationContract]
        RespuestaDTO GenerarCotizacion(int Id, int IdPadre);
        [OperationContract]
        List<TarifarioDTO> ObtPedidoMapProductos(int Id);
        [OperationContract]
        List<PedidoDTO> ObtPedidoPorCotizar();
        [OperationContract]
        List<PedidoDTO> ObtPedidoPorValidar();
        [OperationContract]
        List<PedidoDTO> ObtPedido();
        [OperationContract(Name = "ObtPedidoxId")]
        PedidoDTO ObtPedido(int Id);
        [OperationContract]
        RespuestaDTO EditPedido(PedidoDTO obj);
        [OperationContract]
        RespuestaDTO ElimPedido(int Id);
        [OperationContract]
        RespuestaDTO AprobarPedido(string Id, string Correo, string Comentario, string Usuario);
        [OperationContract]
        RespuestaDTO RechazarPedido(string Id, string Correo, string Comentario, string Usuario);
        [OperationContract]
        RespuestaDTO ObservarPedido(string Id, string Correo, string Comentario, string Usuario);
        [OperationContract]
        List<PedidoDTO> ObtAllPedido(string desc);
        #endregion

        #region DetallePedido
        [OperationContract]
        DetallePedidoDTO ObtDetallePedido(int Id);
        [OperationContract]
        RespuestaDTO EditDetallePedido(DetallePedidoDTO obj);
        [OperationContract]
        RespuestaDTO ElimDetallePedido(int Id);
        #endregion

        #region Cotizacion
        [OperationContract]
        List<CotizacionDTO> ObtAllCotizacion(string desc);
        [OperationContract]
        RespuestaDTO GenerarOC(int Id, string usuario);
        [OperationContract]
        RespuestaDTO EnviarCotizacion(int Id);
        [OperationContract]
        List<DetalleCotizacionDTO> AjustarCotizacion(int IdProv, int IdCotizacion);
        [OperationContract]
        List<CotizacionDTO> ObtCotizacion();
        [OperationContract(Name = "ObtCotizacionxId")]
        CotizacionDTO ObtCotizacion(int Id);
        [OperationContract]
        RespuestaDTO EditCotizacion(CotizacionDTO obj);
        [OperationContract]
        RespuestaDTO ElimCotizacion(int Id);
        #endregion

        #region DetalleCotizacion
        [OperationContract]
        DetalleCotizacionDTO ObtDetalleCotizacion(int Id);
        [OperationContract]
        RespuestaDTO EditDetalleCotizacion(DetalleCotizacionDTO obj);
        [OperationContract]
        RespuestaDTO ElimDetalleCotizacion(int Id);
        #endregion

        #region OrdenCompra
        [OperationContract]
        List<OrdenCompraDTO> ObtAllOrdenCompra(string desc);
        [OperationContract]
        RespuestaDTO FactOrdenCompra(int Id, string Factura);
        [OperationContract]
        RespuestaDTO EnviarOrdenCompra(int Id);
        [OperationContract]
        List<OrdenCompraDTO> ObtOrdenCompraPorValidar();
        [OperationContract]
        List<OrdenCompraDTO> ObtOrdenCompra();
        [OperationContract(Name = "ObtOrdenCompraxId")]
        OrdenCompraDTO ObtOrdenCompra(int Id);
        [OperationContract]
        RespuestaDTO EditOrdenCompra(OrdenCompraDTO obj);
        [OperationContract]
        RespuestaDTO ElimOrdenCompra(int Id);
        [OperationContract]
        RespuestaDTO AprobarOrdenCompra(string Id, string Correo, string Comentario, string Usuario);
        [OperationContract]
        RespuestaDTO RechazarOrdenCompra(string Id, string Correo, string Comentario, string Usuario);
        [OperationContract]
        RespuestaDTO ObservarOrdenCompra(string Id, string Correo, string Comentario, string Usuario);
        [OperationContract]
        List<IMP_ORDENCOMPRA> ImprimirOrdenCompra(int Id);
        #endregion

        #region DetalleOrdenCompra
        [OperationContract]
        DetalleOrdenCompraDTO ObtDetalleOrdenCompra(int Id);
        [OperationContract]
        RespuestaDTO EditDetalleOrdenCompra(DetalleOrdenCompraDTO obj);
        [OperationContract]
        RespuestaDTO ElimDetalleOrdenCompra(int Id);
        #endregion

        #region OrdenCompraDoc
        [OperationContract]
        DocumentoDTO ObtOrdenCompraDocNombre(int Id);
        [OperationContract]
        RespuestaDTO EditOrdenCompraDoc(OrdenCompraDocDTO obj);
        [OperationContract]
        RespuestaDTO ElimOrdenCompraDoc(int Id);
        #endregion

        #region Ubigeo
        [OperationContract]
        List<TablaDTO> ObtPais();

        [OperationContract]
        List<UbigeoDTO> ObtDepartamento(int IdPais);

        [OperationContract]
        List<UbigeoDTO> ObtProvincia(int IdDep);

        [OperationContract]
        List<UbigeoDTO> ObtDistrito(int IdProv);
        #endregion

        #region Impuesto
        [OperationContract]
        List<ImpuestoDTO> ObtImpuesto();

        [OperationContract(Name = "ObtImpuestoxId")]
        ImpuestoDTO ObtImpuesto(int Id);

        [OperationContract]
        RespuestaDTO EditImpuesto(ImpuestoDTO obj);

        [OperationContract]
        RespuestaDTO ElimImpuesto(int Id);
        #endregion

        #region ImpuestoProveedor
        [OperationContract]
        ImpuestoProveedorDTO ObtImpuestoProveedor(int Id);
        [OperationContract]
        RespuestaDTO EditImpuestoProveedor(ImpuestoProveedorDTO obj);
        [OperationContract]
        RespuestaDTO ElimImpuestoProveedor(int Id);
        #endregion

        #region Banco
        [OperationContract]
        List<BancoDTO> ObtBanco();

        [OperationContract(Name = "ObtBancoxId")]
        BancoDTO ObtBanco(int Id);

        [OperationContract]
        RespuestaDTO EditBanco(BancoDTO obj);

        [OperationContract]
        RespuestaDTO ElimBanco(int Id);
        #endregion

        #region Empresa
        [OperationContract]
        List<EmpresaDTO> ObtEmpresa();

        [OperationContract(Name = "ObtEmpresaxId")]
        EmpresaDTO ObtEmpresa(int Id);

        [OperationContract]
        RespuestaDTO EditEmpresa(EmpresaDTO obj);

        [OperationContract]
        RespuestaDTO ElimEmpresa(int Id);
        #endregion

        #region Provision
        [OperationContract]
        List<ProvisionDTO> ObtProvision();

        [OperationContract(Name = "ObtProvisionxId")]
        ProvisionDTO ObtProvision(int Id);

        [OperationContract]
        RespuestaDTO EditProvision(ProvisionDTO obj);

        [OperationContract]
        RespuestaDTO ElimProvision(int Id);
        #endregion

        #region Tarea
        [OperationContract]
        List<TareaDTO> ObtTarea();

        [OperationContract(Name = "ObtTareaxId")]
        TareaDTO ObtTarea(int Id);

        [OperationContract]
        RespuestaDTO EditTarea(TareaDTO obj);

        [OperationContract]
        RespuestaDTO ElimTarea(int Id);
        #endregion

    }
}