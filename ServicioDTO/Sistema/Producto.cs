using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace com.msc.services.dto
{
    [DataContract]
    [KnownType(typeof(FamiliaDTO))]
    [KnownType(typeof(SubFamiliaDTO))]
    [KnownType(typeof(TarifarioDTO))]
    public class ProductoDTO
    {
        public ProductoDTO()
        {
            Tarifarios = new List<TarifarioDTO>();
        }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public TablaDTO TipoServicio { get; set; }

        [DataMember]
        public TablaDTO Grupo { get; set; }

        [DataMember]
        public FamiliaDTO Familia { get; set; }

        [DataMember]
        public SubFamiliaDTO SubFamilia { get; set; }

        [DataMember]
        public TablaDTO UnidadMedida { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string Abreviatura { get; set; }

        [DataMember]
        public string Observaciones { get; set; }

        public List<TarifarioDTO> Tarifarios { get; set; }
    }
}
