using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.msc.infraestructure.entities.mvc
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int MaxGrupoPage { get; set; }

        public int CurrentGroup
        {
            get { return (int)Math.Ceiling((decimal)CurrentPage / MaxGrupoPage); }
        }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }

}