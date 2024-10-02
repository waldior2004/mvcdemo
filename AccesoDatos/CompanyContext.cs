using System.Data.Entity;
using com.msc.infraestructure.entities;

namespace com.msc.infraestructure.dal
{
    public partial class CompanyContext : DbContext
    {
        public CompanyContext()
            : base("name=CompanyContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<TipoControl> TipoControls { get; set; }
        public DbSet<Pagina> Paginas { get; set; }
        public DbSet<Control> Controls { get; set; }
        public DbSet<PerfilControl> PerfilControls { get; set; }
        public DbSet<Perfil> Perfils { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfils { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<GrupoTabla> GrupoTablas { get; set; }
        public DbSet<Tabla> Tablas { get; set; }
        public DbSet<Externo> Externos { get; set; }
        public DbSet<ExternoPerfil> ExternoPerfils { get; set; }
        public DbSet<TarifaCE> TarifaCEs { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<TarifaCEDoc> TarifaCEDocs { get; set; }
        public DbSet<Bitacora> Bitacoras { get; set; }
        public DbSet<CondEspeCli> CondEspeClis { get; set; }
        public DbSet<CondEspeCliDoc> CondEspeCliDocs { get; set; }
        public DbSet<CondEspeCliDia> CondEspeCliDias { get; set; }
        public DbSet<CondEspeCliDetalle> CondEspeCliDetalles { get; set; }
        public DbSet<CargaLiquiC> CargaLiquiCs { get; set; }
        public DbSet<CargaLiquiD> CargaLiquiDs { get; set; }
        public DbSet<CargaLiqui> CargaLiquis { get; set; }
        public DbSet<Nave> Naves { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<Puerto> Puertos { get; set; }
        public DbSet<Contenedor> Contenedors { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Familia> Familias { get; set; }
        public DbSet<SubFamilia> SubFamilias { get; set; }
        public DbSet<Tarifario> Tarifario { get; set; }
        public DbSet<TarifarioDoc> TarifarioDocs { get; set; }
        public DbSet<ContactoProveedor> ContactoProveedores { get; set; }
        public DbSet<DatoComercial> DatoComerciales { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Sucursal> Sucursals { get; set; }
        public DbSet<CentroCosto> CentroCostos { get; set; }
        public DbSet<AreaSolicitante> AreaSolicitantes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<TempoPedido> TempoPedidos { get; set; }
        public DbSet<Cotizacion> Cotizacions { get; set; }
        public DbSet<DetalleCotizacion> DetalleCotizacions { get; set; }
        public DbSet<OrdenCompra> OrdenCompras { get; set; }
        public DbSet<DetalleOrdenCompra> DetalleOrdenCompras { get; set; }
        public DbSet<V_Tarifario> VTarifarios { get; set; }
        public DbSet<OrdenCompraDoc> OrdenCompraDocs { get; set; }
        public DbSet<Ubigeo> Ubigeos { get; set; }
        public DbSet<Impuesto> Impuestos { get; set; }
        public DbSet<ImpuestoProveedor> ImpuestoProveedors { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Provision> Provisions { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        public DbSet<CargaLiquiDistribucion> CargaLiquiDistribuciones { get; set; }
    }
}
