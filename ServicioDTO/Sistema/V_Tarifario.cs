using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [KnownType(typeof(ProveedorDTO))]
    [KnownType(typeof(ProductoDTO))]
    [KnownType(typeof(TablaDTO))]
    public class V_TarifarioDTO
    {
        public V_TarifarioDTO()
        {

        }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public ProveedorDTO Proveedor { get; set; }

        [DataMember]
        public ProductoDTO Producto { get; set; }

        [DataMember]
        public TablaDTO Moneda { get; set; }

        [DataMember]
        public TablaDTO Estado { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public Decimal Precio { get; set; }

        [DataMember]
        public DateTime InicioVigencia { get; set; }

        [DataMember]
        public DateTime FinVigencia { get; set; }

    }
}
