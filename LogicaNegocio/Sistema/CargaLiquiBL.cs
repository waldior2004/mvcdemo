using System.Collections.Generic;
using com.msc.infraestructure.dal;
using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System.Data;
using System;
using System.Linq;
using System.Collections;
using System.Globalization;

namespace com.msc.infraestructure.biz
{
    public class CargaLiquiBL
    {
        private Repository _repositorio;

        public CargaLiquiBL()
        {
            _repositorio = new Repository();
        }

        public CargaLiqui ObtCargaLiqui(int Id)
        {
            return _repositorio.ObtCargaLiqui(Id);
        }
        
    }
}
