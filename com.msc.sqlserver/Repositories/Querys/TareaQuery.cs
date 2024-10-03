using com.msc.wcf.entities.Sistema;
using MediatR;
using System.Collections.Generic;

namespace com.msc.sqlserver.Repositories.Querys
{
    public class TareaQuery : IRequest<IEnumerable<TareaDTO>>
    {
        public TareaQuery()
        {

        }
    }
}
