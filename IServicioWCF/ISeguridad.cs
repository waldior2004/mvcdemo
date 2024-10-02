using com.msc.services.dto;
using System.Collections.Generic;
using System.ServiceModel;

namespace com.msc.services.interfaces
{
    [ServiceContract]
    public partial interface ISeguridad
    {
        [OperationContract]
        ExternoDTO GetUserExternalAuthenticated(string user);

        [OperationContract]
        UsuarioDTO GetUserAuthenticated(string user);

        [OperationContract]
        RespuestaDTO Authenticate(LoginDTO log);

        [OperationContract]
        RespuestaDTO AuthenticateExterno(LoginDTO log);

        [OperationContract]
        RespuestaDTO ChangePassword(string Usuario, string Clave);

        #region TipoControl
        [OperationContract]
        List<TipoControlDTO> ObtTipoControl();

        [OperationContract(Name = "ObtTipoControlxId")]
        TipoControlDTO ObtTipoControl(int Id);

        [OperationContract]
        RespuestaDTO EditTipoControl(TipoControlDTO obj);

        [OperationContract]
        RespuestaDTO ElimTipoControl(int Id);
        #endregion

        #region Pagina
        [OperationContract]
        List<PaginaDTO> ObtPagina();
        [OperationContract(Name = "ObtPaginaxId")]
        PaginaDTO ObtPagina(int Id);
        [OperationContract]
        RespuestaDTO EditPagina(PaginaDTO obj);
        [OperationContract]
        RespuestaDTO ElimPagina(int Id);
        #endregion

        #region Control
        [OperationContract]
        List<ControlDTO> ObtControl();

        [OperationContract(Name = "ObtControlxId")]
        ControlDTO ObtControl(int Id);

        [OperationContract]
        RespuestaDTO EditControl(ControlDTO obj);

        [OperationContract]
        RespuestaDTO ElimControl(int Id);
        #endregion

        #region Rol
        [OperationContract]
        List<RolDTO> ObtRol();

        [OperationContract(Name = "ObtRolxId")]
        RolDTO ObtRol(int Id);

        [OperationContract]
        RespuestaDTO EditRol(RolDTO obj);

        [OperationContract]
        RespuestaDTO ElimRol(int Id);
        #endregion

        #region Perfil
        [OperationContract]
        List<PerfilDTO> ObtPerfil();

        [OperationContract(Name = "ObtPerfilxId")]
        PerfilDTO ObtPerfil(int Id);

        [OperationContract]
        RespuestaDTO EditPerfil(PerfilDTO obj);

        [OperationContract]
        RespuestaDTO ElimPerfil(int Id);
        #endregion

        #region PerfilControl
        [OperationContract]
        RespuestaDTO EditPerfilControl(PerfilControlDTO obj);
        [OperationContract]
        RespuestaDTO ElimPerfilControl(int Id);
        #endregion

        #region Usuario
        [OperationContract]
        List<UsuarioDTO> ObtUsuario();
        [OperationContract(Name = "ObtUsuarioxId")]
        UsuarioDTO ObtUsuario(int Id);
        [OperationContract]
        RespuestaDTO EditUsuario(UsuarioDTO obj);
        [OperationContract]
        RespuestaDTO ElimUsuario(int Id);
        #endregion

        #region UsuarioPerfil
        [OperationContract]
        RespuestaDTO EditUsuarioPerfil(UsuarioPerfilDTO obj);
        [OperationContract]
        RespuestaDTO ElimUsuarioPerfil(int Id);
        #endregion

        #region ExternoPerfil
        [OperationContract]
        RespuestaDTO EditExternoPerfil(ExternoPerfilDTO obj);
        [OperationContract]
        RespuestaDTO ElimExternoPerfil(int Id);
        #endregion

    }
}
