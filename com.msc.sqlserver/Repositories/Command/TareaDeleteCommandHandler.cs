using com.msc.usecase.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace com.msc.sqlserver.Repositories.Command
{
    public class TareaDeleteCommandHandler : IRequestHandler<TareaDeleteCommand, int>
    {
        private readonly SqlContext context;

        public TareaDeleteCommandHandler(IDbContext _context)
        {
            context = (_context as SqlContext);
        }

        public Task<int> Handle(TareaDeleteCommand request, CancellationToken cancellationToken)
        {
            var exists = context.Tareas.Where(p => p.Id == request.id).FirstOrDefault();
            context.Tareas.Remove(exists);

            context.SaveChanges();

            return Task.FromResult(0);
        }
    }
}
