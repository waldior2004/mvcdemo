﻿using com.msc.infraestructure.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.msc.services.dto.DataMapping
{
    public static partial class DataMapping
    {
        public static EmpresaDTO GetEmpresaDTO(this Empresa source)
        {
            var objR = source.CreateMap<Empresa, EmpresaDTO>();
            return objR;
        }
        public static Empresa SetEmpresa(this EmpresaDTO source)
        {
            var objR = source.CreateMap<EmpresaDTO, Empresa>();
            return objR;
        }
    }
}
