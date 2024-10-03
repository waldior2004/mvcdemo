using com.msc.domain.entities.Sistema;
using com.msc.wcf.entities;
using com.msc.wcf.entities.Sistema;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace security.back.usecase
{
    public interface ITareaManagementUseCase
    {
        Task<IEnumerable<TareaDTO>> Get();
        Task<TareaDTO> Get(int id);
        Task<RespuestaDTO> New(Tarea obj);
        Task<RespuestaDTO> Edit(Tarea obj);
        Task<RespuestaDTO> Delete(int id);
    }
}
