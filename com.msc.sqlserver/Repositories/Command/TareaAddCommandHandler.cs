using AutoMapper;
using com.msc.sqlserver.entities.Sistema;
using com.msc.usecase.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace com.msc.sqlserver.Repositories.Command
{
    public class TareaAddCommandHandler : IRequestHandler<TareaAddCommand, int>
    {
        private readonly SqlContext context;
        private readonly IMapper mapper;

        public TareaAddCommandHandler(IDbContext _context, IMapper _mapper)
        {
            context = (_context as SqlContext);
            mapper = _mapper;
        }

        public Task<int> Handle(TareaAddCommand request, CancellationToken cancellationToken)
        {
            var u = mapper.Map<TareaADO>(request.input);
            u.AudActivo = 1;

            context.Tareas.Add(u);

            context.SaveChanges();

            return Task.FromResult(0);
        }
    }
}
