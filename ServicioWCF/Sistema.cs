using AutoMapper;
using com.msc.domain.entities.Sistema;
using com.msc.infraestructure.biz;
using com.msc.mapper;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using com.msc.sqlserver;
using com.msc.sqlserver.Repositories;
using com.msc.usecase.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using security.back.usecase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.msc.services.implementations
{
    public partial class SistemaService : ISistema
    {

        private ExternoBL _externoLogic;
        private readonly ITareaManagementUseCase _tareaManagementUseCase;
        private readonly IMapper _mapper;

        public SistemaService()
        {
            _externoLogic = new ExternoBL();

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IDbContext, SqlContext>();
            serviceCollection.AddMediatR(typeof(SqlContext));
            serviceCollection.AddScoped<ITareaRepository, TareaRepository>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            var types = new List<Type>();
            serviceCollection.AddAutoMapper((provider, cfg) =>
            {
                var apirest = new TareaMapper();
                types.Add(apirest.GetType());
                cfg.AddProfile(apirest);

            }, types);
            serviceCollection.AddScoped<ITareaManagementUseCase, TareaManagementUseCase>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            _tareaManagementUseCase = serviceProvider.GetService<ITareaManagementUseCase>();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        #region Externo
        public ExternoDTO ObtExternoPorUserName(string userName)
        {
            var objR = _externoLogic.ObtExterno(userName);
            return objR.GetExternoDTO();
        }


        public List<ExternoDTO> ObtExterno()
        {
            var list = _externoLogic.ObtExterno();
            var lstR = new List<ExternoDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetExternoDTO());
            }
            return lstR;
        }

        public ExternoDTO ObtExterno(int Id)
        {
            var objR = _externoLogic.ObtExterno(Id);
            return objR.GetExternoDTO();
        }

        public RespuestaDTO ResetKeyExterno(int Id)
        {
            var objR = _externoLogic.ResetKeyExterno(Id);
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO EditExterno(ExternoDTO obj)
        {
            var objR = _externoLogic.EditExterno(obj.SetExterno());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimExterno(int Id)
        {
            var objR = _externoLogic.ElimExterno(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        /*
        //#region Tarea
        //public List<TareaDTO> ObtTarea()
        //{
        //    var list = _tareaLogic.ObtTarea();
        //    var lstR = new List<TareaDTO>();
        //    foreach (var item in list)
        //    {
        //        lstR.Add(item.GetTareaDTO());
        //    }
        //    return lstR;
        //}

        //public TareaDTO ObtTarea(int Id)
        //{
        //    var objR = _tareaLogic.ObtTarea(Id);
        //    return objR.GetTareaDTO();
        //}

        //public RespuestaDTO EditTarea(TareaDTO obj)
        //{
        //    var objR = _tareaLogic.EditTarea(obj.SetTarea());
        //    return objR.GetRespuestaDTO();
        //}

        //public RespuestaDTO ElimTarea(int Id)
        //{
        //    var objR = _tareaLogic.ElimTarea(Id);
        //    return objR.GetRespuestaDTO();
        //}
        //#endregion
        */

        #region Tarea - Arquitectura Hexagonal
        public List<wcf.entities.Sistema.TareaDTO> ObtTarea()
        {
            try
            {
                var listado = _tareaManagementUseCase.Get().Result;
                return listado.ToList();
            }
            catch
            {
                return null;
            }
        }

        public wcf.entities.Sistema.TareaDTO ObtTarea(int Id)
        {
            try
            {
                var objeto = _tareaManagementUseCase.Get(Id).Result;
                return objeto;
            }
            catch
            {
                return null;
            }
        }

        public wcf.entities.RespuestaDTO EditTarea(wcf.entities.Sistema.TareaDTO obj)
        {
            try
            {
                var resultado = obj.Id == 0 ? 
                    _tareaManagementUseCase.New(_mapper.Map<Tarea>(obj)).Result : 
                    _tareaManagementUseCase.Edit(_mapper.Map<Tarea>(obj)).Result;

                return resultado;
            }
            catch (Exception ex)
            {
                return new wcf.entities.RespuestaDTO
                {
                    Id = ex.HResult,
                    Descripcion = ex.Message,
                    Message = (ex.InnerException != null ? ex.InnerException.Message : ""),
                    Aplicacion = ex.Source,
                    TipoError = ex.HelpLink,
                    PilaError = ex.StackTrace
                };
            }
        }

        public wcf.entities.RespuestaDTO ElimTarea(int Id)
        {
            try
            { 
                var resultado = _tareaManagementUseCase.Delete(Id).Result;
                return resultado;
            }
            catch (Exception ex)
            {
                return new wcf.entities.RespuestaDTO
                {
                    Id = ex.HResult,
                    Descripcion = ex.Message,
                    Message = (ex.InnerException != null ? ex.InnerException.Message : ""),
                    Aplicacion = ex.Source,
                    TipoError = ex.HelpLink,
                    PilaError = ex.StackTrace
                };
            }
        }
        #endregion

    }
}