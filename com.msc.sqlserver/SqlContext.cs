using com.msc.sqlserver.entities.Sistema;
using System.Data.Entity;

namespace com.msc.sqlserver
{
    public class SqlContext : DbContext, ISqlContext
    {
        public DbSet<TareaADO> Tareas { get; set; }

        public SqlContext() : base("name=CompanyContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

    }
}
