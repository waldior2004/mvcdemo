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
        public static DatoComercialDTO GetDatoComercialDTO(this DatoComercial source)
        {
            var objR = source.CreateMap<DatoComercial, DatoComercialDTO>();

            if (source.Pais != null)
                objR.Pais = source.Pais.CreateMap<Tabla, TablaDTO>();
            else
                objR.Pais = new TablaDTO { Id = source.IdPais2 };

            if (source.Banco != null)
                objR.Banco = source.Banco.CreateMap<Banco, BancoDTO>();
            else
                objR.Banco = new BancoDTO { Id = source.IdBanco };

            if (source.TipoCuenta != null)
                objR.TipoCuenta = source.TipoCuenta.CreateMap<Tabla, TablaDTO>();
            else
                objR.TipoCuenta = new TablaDTO { Id = source.IdTipoCuenta };

            if (source.TipoInterlocutor != null)
                objR.TipoInterlocutor = source.TipoInterlocutor.CreateMap<Tabla, TablaDTO>();
            else
                objR.TipoInterlocutor = new TablaDTO { Id = source.IdTipoInterlocutor };

            return objR;
        }

        public static DatoComercial SetDatoComercial(this DatoComercialDTO source)
        {
            var objR = source.CreateMap<DatoComercialDTO, DatoComercial>();
            if (source.Pais != null)
            {
                objR.IdPais2 = source.Pais.Id;
                objR.Pais = source.Pais.CreateMap<TablaDTO, Tabla>();
            }
            if (source.Banco != null)
            {
                objR.IdBanco = source.Banco.Id;
                objR.Banco = source.Banco.CreateMap<BancoDTO, Banco>();
            }
            if (source.TipoCuenta != null)
            {
                objR.IdTipoCuenta = source.TipoCuenta.Id;
                objR.TipoCuenta = source.TipoCuenta.CreateMap<TablaDTO, Tabla>();
            }
            if (source.TipoInterlocutor != null)
            {
                objR.IdTipoInterlocutor = source.TipoInterlocutor.Id;
                objR.TipoInterlocutor = source.TipoInterlocutor.CreateMap<TablaDTO, Tabla>();
            }

            return objR;
        }
    }
}
