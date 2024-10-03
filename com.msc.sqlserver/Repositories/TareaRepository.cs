using com.msc.domain.entities.Sistema;
using com.msc.sqlserver.Repositories.Command;
using com.msc.sqlserver.Repositories.Querys;
using com.msc.usecase.Interfaces;
using com.msc.wcf.entities.Sistema;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.msc.sqlserver.Repositories
{
    public class TareaRepository : ITareaRepository
    {
        private readonly IMediator mediator;

        public TareaRepository(IMediator _mediator)
        {
            mediator = _mediator;
        }

        public async Task<IEnumerable<TareaDTO>> Get()
        {
            return await mediator.Send(new TareaQuery());
        }

        public async Task<TareaDTO> Get(int id)
        {
            return await mediator.Send(new TareaByIdQuery(id));
        }

        public async Task<int> Add(Tarea obj)
        {
            return await mediator.Send(new TareaAddCommand(obj));
        }

        public async Task<int> Edit(Tarea obj)
        {
            return await mediator.Send(new TareaEditCommand(obj));
        }

        public async Task<int> Delete(int id)
        {
            return await mediator.Send(new TareaDeleteCommand(id));
        }

    }
}
