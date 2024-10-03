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
        public DbSet<Externo> Externos { get; set; }
        public DbSet<ExternoPerfil> ExternoPerfils { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
    }
}
