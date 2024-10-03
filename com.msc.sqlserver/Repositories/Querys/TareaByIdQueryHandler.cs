using AutoMapper;
using com.msc.sqlserver.entities.Sistema;
using com.msc.wcf.entities.Sistema;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace com.msc.sqlserver.Repositories.Querys
{
    public class TareaByIdQueryHandler : IRequestHandler<TareaByIdQuery, TareaDTO>
    {
        private readonly string connectionstring;
        private readonly IMapper mapper;

        public TareaByIdQueryHandler(IMapper _mapper)
        {
            connectionstring = "data source=.;initial catalog=COMPANY;user id=sa;Password=03071982;MultipleActiveResultSets=True;";
            mapper = _mapper;
        }

        public async Task<TareaDTO> Handle(TareaByIdQuery request, CancellationToken cancellationToken)
        {
            var query = $@"select ID as Id, TITULO as Titulo, DESCRIPCION as Descripcion, COMPLETADO as Completado, AUD_FECMOD as AudUpdate, AUD_ACTIVE as AudActivo 
                        from SISTEMA.T_TAREA
                        where Id = {request.Id} and AUD_ACTIVE = 1";

            using (var connection = new SqlConnection(connectionstring))
            {
                var lookup1 = new Dictionary<int, TareaADO>();
                var tarea = await connection.QueryAsync<TareaADO>(query);
                var obj = mapper.Map<TareaDTO>(tarea.FirstOrDefault());

                return obj;
            }
        }
    }
}
