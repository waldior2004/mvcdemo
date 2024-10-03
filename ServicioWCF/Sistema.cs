using com.msc.infraestructure.biz;
using com.msc.infraestructure.entities;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.Collections.Generic;

namespace com.msc.services.implementations
{
    public partial class SistemaService : ISistema
    {
        
        private ExternoBL _externoLogic;
        private TareaBL _tareaLogic;

        public SistemaService()
        {
            _externoLogic = new ExternoBL();
            _tareaLogic = new TareaBL();
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

        #region Tarea
        public List<TareaDTO> ObtTarea()
        {
            var list = _tareaLogic.ObtTarea();
            var lstR = new List<TareaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetTareaDTO());
            }
            return lstR;
        }

        public TareaDTO ObtTarea(int Id)
        {
            var objR = _tareaLogic.ObtTarea(Id);
            return objR.GetTareaDTO();
        }

        public RespuestaDTO EditTarea(TareaDTO obj)
        {
            var objR = _tareaLogic.EditTarea(obj.SetTarea());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimTarea(int Id)
        {
            var objR = _tareaLogic.ElimTarea(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

    }
}