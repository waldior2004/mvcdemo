using com.msc.services.dto;
using System.Collections.Generic;
using System.ServiceModel;

namespace com.msc.services.interfaces
{
    [ServiceContract]
    public partial interface ISistema
    {

        #region Externo
        [OperationContract]
        List<ExternoDTO> ObtExterno();

        [OperationContract(Name = "ObtExternoxId")]
        ExternoDTO ObtExterno(int Id);

        [OperationContract]
        RespuestaDTO ResetKeyExterno(int Id);

        [OperationContract]
        RespuestaDTO EditExterno(ExternoDTO obj);

        [OperationContract]
        RespuestaDTO ElimExterno(int Id);
        #endregion

        #region Tarea
        [OperationContract]
        List<TareaDTO> ObtTarea();

        [OperationContract(Name = "ObtTareaxId")]
        TareaDTO ObtTarea(int Id);

        [OperationContract]
        RespuestaDTO EditTarea(TareaDTO obj);

        [OperationContract]
        RespuestaDTO ElimTarea(int Id);
        #endregion

    }
}