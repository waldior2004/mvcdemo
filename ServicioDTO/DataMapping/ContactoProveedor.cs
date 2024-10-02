using com.msc.infraestructure.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static ContactoProveedorDTO GetContactoProveedorDTO(this ContactoProveedor source)
        {
            var objR = source.CreateMap<ContactoProveedor, ContactoProveedorDTO>();

            if (source.Cargo != null)
                objR.Cargo = source.Cargo.CreateMap<Tabla, TablaDTO>();
            else if (source.IdCargo != null)
                objR.Cargo = new TablaDTO { Id = source.IdCargo };

            return objR;
        }

        public static ContactoProveedor SetContactoProveedor(this ContactoProveedorDTO source)
        {
            var objR = source.CreateMap<ContactoProveedorDTO, ContactoProveedor>();
            if (source.Cargo != null)
            {
                objR.IdCargo = source.Cargo.Id;
                objR.Cargo = source.Cargo.CreateMap<TablaDTO, Tabla>();
            }

            return objR;
        }
    }
}
