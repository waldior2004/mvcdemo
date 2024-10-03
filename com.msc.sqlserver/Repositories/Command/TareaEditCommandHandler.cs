using com.msc.usecase.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace com.msc.sqlserver.Repositories.Command
{
    public class TareaEditCommandHandler : IRequestHandler<TareaEditCommand, int>
    {
        private readonly SqlContext context;

        public TareaEditCommandHandler(IDbContext _context)
        {
            context = (_context as SqlContext);
        }

        public Task<int> Handle(TareaEditCommand request, CancellationToken cancellationToken)
        {
            var exists = context.Tareas.Where(p => p.Id == request.input.Id).FirstOrDefault();

            if (exists != null)
            {
                exists.Titulo = request.input.Titulo;
                exists.Descripcion = request.input.Descripcion;
                exists.Completado = request.input.Completado;
                exists.AudUpdate = DateTime.Now;
            }

            context.SaveChanges();

            return Task.FromResult(0);
        }
    }
}
