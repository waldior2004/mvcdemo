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
        /// <summary>
        /// Nombre: ObtTarea
        /// Descripcion: Endpoint para listar las tareas
        /// Parametros Input: null
        /// Parametros Output: List<TareaDTO>
        /// Versiones:
        ///     1.0: Mostrar listado de tareas
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TareaDTO> ObtTarea();

        /// <summary>
        /// Nombre: ObtTareaxId
        /// Descripcion: Endpoint para Listar las tareas por ID
        /// Parametros Input: Id [Entero que identifica al codigo unico de la tarea]
        /// Parametros Output: TareaDTO
        /// Versiones:
        ///     1.0: Mostrar datos de la tarea por su identificador
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract(Name = "ObtTareaxId")]
        TareaDTO ObtTarea(int Id);

        /// <summary>
        /// Nombre: EditTarea
        /// Descripcion: Endpoint para Editar las tareas por ID
        /// Parametros Input: TareaDTO [Objeto que contiene los datos de la tarea por modificar incluido el ID]
        /// Parametros Output: RespuestaDTO
        /// Versiones:
        ///     1.0: Editar datos de la tarea por su identificador
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        RespuestaDTO EditTarea(TareaDTO obj);

        /// <summary>
        /// Nombre: ElimTarea
        /// Descripcion: Endpoint para Eliminar las tareas por ID
        /// Parametros Input: Id [Entero que identifica al codigo unico de la tarea]
        /// Parametros Output: RespuestaDTO
        /// Versiones:
        ///     1.0: Eliminar datos de la tarea por su identificador
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [OperationContract]
        RespuestaDTO ElimTarea(int Id);
        #endregion

    }
}