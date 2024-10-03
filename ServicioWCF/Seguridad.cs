using com.msc.infraestructure.biz;
using com.msc.infraestructure.utils;
using com.msc.services.dto;
using com.msc.services.dto.DataMapping;
using com.msc.services.interfaces;
using System;
using System.Collections.Generic;

namespace com.msc.services.implementations
{
    public partial class SeguridadService : ISeguridad
    {
        private ExternoPerfilBL _externoperfilLogic;
        private PerfilControlBL _perfilcontrolLogic;
        private UsuarioPerfilBL _usuarioperfilLogic;
        private PerfilBL _perfilLogic;
        private RolBL _rolLogic;
        private ControlBL _controlLogic;
        private TipoControlBL _tipocontrolLogic;
        private PaginaBL _paginaLogic;
        private UsuarioBL _usuarioLogic;
        private LoginBL _loginLogic;

        public SeguridadService()
        {
            _externoperfilLogic = new ExternoPerfilBL();
            _usuarioperfilLogic = new UsuarioPerfilBL();
            _perfilcontrolLogic = new PerfilControlBL();
            _perfilLogic = new PerfilBL();
            _rolLogic = new RolBL();
            _controlLogic = new ControlBL();
            _tipocontrolLogic = new TipoControlBL();
            _usuarioLogic = new UsuarioBL();
            _loginLogic = new LoginBL();
            _paginaLogic = new PaginaBL();
        }

        #region Login
        public RespuestaDTO ChangePassword(string Usuario, string Clave)
        {
            try
            {
                var objR = _loginLogic.ChangePassword(Usuario, Clave);
                return objR.GetRespuestaDTO();
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public UsuarioDTO GetUserAuthenticated(string user)
        {
            try
            {
                var objR = _loginLogic.GetUserAuthenticated(user);
                return objR.GetUsuarioDTO();
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public ExternoDTO GetUserExternalAuthenticated(string user)
        {
            try
            {
                var objR = _loginLogic.GetUserExternalAuthenticated(user);
                return objR.GetExternoDTO();
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, null);
                return null;
            }
        }

        public RespuestaDTO Authenticate(LoginDTO log)
        {
            try
            {
                var objR = _loginLogic.Authenticate(log.SetLogin());
                return objR.GetRespuestaDTO();
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, log);
                return null;
            }
        }

        public RespuestaDTO AuthenticateExterno(LoginDTO log)
        {
            try
            {
                var objR = _loginLogic.AuthenticateExterno(log.SetLogin());
                return objR.GetRespuestaDTO();
            }
            catch (Exception ex)
            {
                LogError.PostErrorMessage(ex, log);
                return null;
            }
        }
        #endregion

        #region TipoControl
        public List<TipoControlDTO> ObtTipoControl()
        {
            var list = _tipocontrolLogic.ObtTipoControl();
            var lstR = new List<TipoControlDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetTipoControlDTO());
            }
            return lstR;
        }

        public TipoControlDTO ObtTipoControl(int Id)
        {
            var objR = _tipocontrolLogic.ObtTipoControl(Id);
            return objR.GetTipoControlDTO();
        }

        public RespuestaDTO EditTipoControl(TipoControlDTO obj)
        {
            var objR = _tipocontrolLogic.EditTipoControl(obj.SetTipoControl());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimTipoControl(int Id)
        {
            var objR = _tipocontrolLogic.ElimTipoControl(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Pagina
        public List<PaginaDTO> ObtPagina()
        {
            var list = _paginaLogic.ObtPagina();
            var lstR = new List<PaginaDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetPaginaDTO());
            }
            return lstR;
        }

        public PaginaDTO ObtPagina(int Id)
        {
            var objR = _paginaLogic.ObtPagina(Id);
            return objR.GetPaginaDTO();
        }

        public RespuestaDTO EditPagina(PaginaDTO obj)
        {
            var objR = _paginaLogic.EditPagina(obj.SetPagina());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimPagina(int Id)
        {
            var objR = _paginaLogic.ElimPagina(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Control
        public List<ControlDTO> ObtControl()
        {
            var list = _controlLogic.ObtControl();
            var lstR = new List<ControlDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetControlDTO());
            }
            return lstR;
        }

        public ControlDTO ObtControl(int Id)
        {
            var objR = _controlLogic.ObtControl(Id);
            return objR.GetControlDTO();
        }

        public RespuestaDTO EditControl(ControlDTO obj)
        {
            var objR = _controlLogic.EditControl(obj.SetControl());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimControl(int Id)
        {
            var objR = _controlLogic.ElimControl(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Rol
        public List<RolDTO> ObtRol()
        {
            var list = _rolLogic.ObtRol();
            var lstR = new List<RolDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetRolDTO());
            }
            return lstR;
        }

        public RolDTO ObtRol(int Id)
        {
            var objR = _rolLogic.ObtRol(Id);
            return objR.GetRolDTO();
        }

        public RespuestaDTO EditRol(RolDTO obj)
        {
            var objR = _rolLogic.EditRol(obj.SetRol());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimRol(int Id)
        {
            var objR = _rolLogic.ElimRol(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Perfil
        public List<PerfilDTO> ObtPerfil()
        {
            var list = _perfilLogic.ObtPerfil();
            var lstR = new List<PerfilDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetPerfilDTO());
            }
            return lstR;
        }

        public PerfilDTO ObtPerfil(int Id)
        {
            var objR = _perfilLogic.ObtPerfil(Id);
            return objR.GetPerfilDTO();
        }

        public RespuestaDTO EditPerfil(PerfilDTO obj)
        {
            var objR = _perfilLogic.EditPerfil(obj.SetPerfil());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimPerfil(int Id)
        {
            var objR = _perfilLogic.ElimPerfil(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region PerfilControl
        public RespuestaDTO EditPerfilControl(PerfilControlDTO obj)
        {
            var objR = _perfilcontrolLogic.EditPerfilControl(obj.SetPerfilControl());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimPerfilControl(int Id)
        {
            var objR = _perfilcontrolLogic.ElimPerfilControl(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region Usuario
        public List<UsuarioDTO> ObtUsuario()
        {
            var list = _usuarioLogic.ObtUsuario();
            var lstR = new List<UsuarioDTO>();
            foreach (var item in list)
            {
                lstR.Add(item.GetUsuarioDTO());
            }
            return lstR;
        }

        public UsuarioDTO ObtUsuario(int Id)
        {
            var objR = _usuarioLogic.ObtUsuario(Id);
            return objR.GetUsuarioDTO();
        }

        public RespuestaDTO EditUsuario(UsuarioDTO obj)
        {
            var objR = _usuarioLogic.EditUsuario(obj.SetUsuario());
            return objR.GetRespuestaDTO();
        }

        public RespuestaDTO ElimUsuario(int Id)
        {
            var objR = _usuarioLogic.ElimUsuario(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region UsuarioPerfil
        public RespuestaDTO EditUsuarioPerfil(UsuarioPerfilDTO obj)
        {
            var objR = _usuarioperfilLogic.EditUsuarioPerfil(obj.SetUsuarioPerfil());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimUsuarioPerfil(int Id)
        {
            var objR = _usuarioperfilLogic.ElimUsuarioPerfil(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

        #region ExternoPerfil
        public RespuestaDTO EditExternoPerfil(ExternoPerfilDTO obj)
        {
            var objR = _externoperfilLogic.EditExternoPerfil(obj.SetExternoPerfil());
            return objR.GetRespuestaDTO();
        }
        public RespuestaDTO ElimExternoPerfil(int Id)
        {
            var objR = _externoperfilLogic.ElimExternoPerfil(Id);
            return objR.GetRespuestaDTO();
        }
        #endregion

    }
}
