using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;

namespace com.msc.infraestructure.dal
{
    public partial class Repository
    {

        public Ubigeo ObtUbigeo(int Id)
        {
            Ubigeo lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Ubigeos
                           where p.Id == Id
                           select p).FirstOrDefault();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<Tabla> ObtPais()
        {
            List<Tabla> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Tablas
                           join q in context.GrupoTablas on p.IdGrupoTabla equals q.Id
                           where p.AudActivo == 1 && q.Codigo == "019" && q.AudActivo == 1
                           select p).ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<Ubigeo> ObtDepartamento(int IdPais)
        {
            List<Ubigeo> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Ubigeos
                           where p.IdPais == IdPais && p.CodProvincia == "00" && p.CodDistrito == "00"
                           select p).ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<Ubigeo> ObtProvincia(int IdPais, string CodDep)
        {
            List<Ubigeo> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Ubigeos
                           where p.IdPais == IdPais && p.CodDepartamento == CodDep && p.CodDistrito == "00" && p.CodProvincia != "00"
                           select p).ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public List<Ubigeo> ObtDistrito(int IdPais, string CodDep, string CodProv)
        {
            List<Ubigeo> lst = null;
            try
            {
                using (var context = new CompanyContext())
                {
                    lst = (from p in context.Ubigeos
                           where p.IdPais == IdPais && p.CodDepartamento == CodDep && p.CodProvincia == CodProv && p.CodDistrito != "00"
                           select p).ToList();
                }
                return lst;
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }
    }
}
